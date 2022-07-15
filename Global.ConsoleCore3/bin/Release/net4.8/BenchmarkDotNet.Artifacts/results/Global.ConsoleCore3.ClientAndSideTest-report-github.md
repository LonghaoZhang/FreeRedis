``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1766 (21H2)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT  [AttachedDebugger]
  Job-RLKOES : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT

Runtime=.NET Framework 4.8  

```
|        Method |         Mean |       Error |      StdDev |  Gen 0 | Allocated |
|-------------- |-------------:|------------:|------------:|-------:|----------:|
|     OnlyRedis | 4,625.535 μs | 190.6610 μs | 547.0422 μs | 7.8125 |  88,368 B |
| ClientAndSide |     1.859 μs |   0.0336 μs |   0.0805 μs | 0.2537 |   1,597 B |
