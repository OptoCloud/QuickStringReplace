﻿using System.Runtime.InteropServices;

namespace OptoCloud;

public sealed class QuickStringReplace
{
    private readonly record struct Replacement(int MatchStart, int MatchEnd, string New);

    private readonly string _source;
    private readonly List<Replacement> _replacements = new(8);
    private int _diff;

    public QuickStringReplace(string source)
    {
        _source = source;
    }

    public QuickStringReplace Replace(ReadOnlySpan<char> oldValue, string newValue)
    {
        ArgumentNullException.ThrowIfNull(newValue);

        int oldLength = oldValue.Length;
        if (oldLength <= 0)
        {
            return this;
        }

        ReadOnlySpan<char> source = _source.AsSpan();
        int diff = newValue.Length - oldLength;
        int abs = 0, rel;

        while ((rel = source[abs..].IndexOf(oldValue)) != -1)
        {
            int pos = abs + rel;

            _replacements.Add(new(pos, pos + oldLength, newValue));
            _diff += diff;

            abs = pos + oldLength;
        }

        return this;
    }

    private static int Compare(Replacement left, Replacement right) => left.MatchStart - right.MatchStart;
    private static readonly Comparison<Replacement> Comparison = new(Compare);

    public override string ToString()
    {
        int newLength = _source.Length + _diff;

        return string.Create(newLength, this, static (span, state) =>
        {
            ReadOnlySpan<char> source = state._source.AsSpan();
            Span<Replacement> replacements = CollectionsMarshal.AsSpan(state._replacements);
            replacements.Sort(Comparison);

            int offset = 0, prev = 0;

            for (int i = 0; i < replacements.Length; i++)
            {
                Replacement r = replacements[i];

                int chunk = r.MatchStart - prev;
                if (chunk > 0)
                {
                    source.Slice(prev, chunk).CopyTo(span[offset..]);
                    offset += chunk;
                }

                ReadOnlySpan<char> rNew = r.New.AsSpan();
                if (!rNew.IsEmpty)
                {
                    rNew.CopyTo(span[offset..]);
                    offset += rNew.Length;
                }

                prev = r.MatchEnd;
            }

            int remainder = source.Length - prev;
            if (remainder > 0)
            {
                source[prev..].CopyTo(span[offset..]);
            }
        });
    }
}