# Logger-Benchmark

What is the overhead of building the log message before vs. after, when the log level might be disabled and only examined inside the logger?  Mainly focusing on the presence of string manipulation outside of the logger vs inside.

## Results

``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.134 (1809/October2018Update/Redstone5)
Intel Core i7-8650U CPU 1.90GHz (Kaby Lake R), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=2.2.100
  [Host]     : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  DefaultJob : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT


```
|               Method |      Mean |      Error |     StdDev | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------------- |----------:|-----------:|-----------:|------------:|------------:|------------:|--------------------:|
|     ErrorInterpolate | 583.50 ns | 11.4309 ns | 15.6468 ns |      0.1154 |           - |           - |               488 B |
|   VerboseInterpolate | 582.23 ns |  3.7443 ns |  3.3193 ns |      0.1154 |           - |           - |               488 B |
|   ErrorFormatMessage | 594.32 ns |  8.0660 ns |  7.1503 ns |      0.1268 |           - |           - |               536 B |
| VerboseFormatMessage |  25.03 ns |  0.6550 ns |  0.9600 ns |      0.0229 |           - |           - |                96 B |