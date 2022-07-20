``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1766 (21H2)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT  [AttachedDebugger]
  Job-RLKOES : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT

Runtime=.NET Framework 4.8  

```
|        Method |     Mean |    Error |   StdDev |   Median | Allocated |
|-------------- |---------:|---------:|---------:|---------:|----------:|
| LoaclAndRedis | 19.82 μs | 3.298 μs | 9.357 μs | 17.10 μs |         - |
