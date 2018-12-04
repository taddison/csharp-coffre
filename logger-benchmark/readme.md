# Logger-Benchmark

What is the overhead of building the log message before vs. after, when the log level might be disabled and only examined inside the logger?  Mainly focusing on the presence of string manipulation outside of the logger vs inside.

## Results

``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.134 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.1.300
  [Host]     : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.0 (CoreCLR 4.6.26515.07, CoreFX 4.6.26515.06), 64bit RyuJIT


```
|               Method |      Mean |      Error |     StdDev |    Median | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |----------:|-----------:|-----------:|----------:|------------:|------------:|------------:|--------------------:|
|     ErrorInterpolate | 639.23 ns | 18.3886 ns | 54.2192 ns | 620.31 ns |      0.1154 |           - |           - |               488 B |
|   VerboseInterpolate | 619.80 ns | 26.2803 ns | 48.0550 ns | 605.18 ns |      0.1154 |           - |           - |               488 B |
|   ErrorFormatMessage | 641.25 ns | 12.8306 ns | 26.2095 ns | 637.94 ns |      0.1268 |           - |           - |               536 B |
| VerboseFormatMessage |  25.86 ns |  0.5079 ns |  0.4751 ns |  26.01 ns |      0.0228 |           - |           - |                96 B |