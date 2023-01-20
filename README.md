# QuickStringReplace

QuickStringReplace allows for replacing multiple occurrences of a substring with a new value in a given source string.
It is benchmarked to be twice as fast as String.Replace(), and twice as memory efficient as StringBuilder.Replace().

Thanks to [Ero](https://github.com/just-ero) for helping out with optimizing the code.

Original Gists can be found here:
[QuickStringReplacer](https://gist.github.com/OptoCloud/23a0acb9cd6c692c07dbdf5ea9651baa)
[QuickReplace (Fork)](https://gist.github.com/just-ero/82e6737d726a64149fe2ba024cdd0bf7)

Email template example is from [Lee Munroe's Responsive HTML Email Template](https://github.com/leemunroe/responsive-html-email-template)

## Usage

```csharp
var source = "This was a {{word}} string";

var result = new QuickStringReplace(source)
    .Replace("was", "is")
    .Replace("{{word}}", "test")
    .ToString();

Console.WriteLine(result);
```

## Benchmarks

``` ini
BenchmarkDotNet=v0.13.4, OS=Windows 10 (10.0.19045.2486)
11th Gen Intel Core i7-11800H 2.30GHz, 1 CPU, 16 logical and 8 physical cores
.NET SDK=7.0.101
  [Host]     : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.1 (7.0.122.56804), X64 RyuJIT AVX2


|                   Method |       Mean |     Error |    StdDev | Ratio | RatioSD |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|------------------------- |-----------:|----------:|----------:|------:|--------:|-------:|-------:|----------:|------------:|
|        StringReplaceTest |   9.428 us | 0.1880 us | 0.2636 us |  1.00 |    0.00 | 9.4604 | 1.0376 | 116.36 KB |        1.00 |
| StringBuilderReplaceTest | 199.821 us | 0.4280 us | 0.3794 us | 21.12 |    0.82 | 2.6855 | 0.2441 |  33.74 KB |        0.29 |
|   QuickStringReplaceTest |   4.625 us | 0.0436 us | 0.0387 us |  0.49 |    0.02 | 1.3809 | 0.0839 |  17.01 KB |        0.15 |
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details