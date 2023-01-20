using OptoCloud;

Console.ReadKey();
var summary = BenchmarkDotNet.Running.BenchmarkRunner.Run<StringReplaceBenchmark>();
Console.ReadKey();