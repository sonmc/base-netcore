```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3271/22H2/2022Update)
Intel Core i3-10105 CPU 3.70GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.20 (6.0.2023.32017), X64 RyuJIT AVX2


```
|                      Method |     Mean |    Error |   StdDev |   Median |   Gen0 | Allocated |
|---------------------------- |---------:|---------:|---------:|---------:|-------:|----------:|
|  CheckPermissionS1Benchmark | 524.5 μs | 10.43 μs | 23.75 μs | 512.1 μs | 8.7891 |  29.77 KB |
| CheckPermissionS21Benchmark | 631.2 μs |  8.12 μs |  7.59 μs | 630.1 μs | 8.7891 |  27.62 KB |
