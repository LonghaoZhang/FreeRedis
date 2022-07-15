``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.1766 (21H2)
11th Gen Intel Core i7-11370H 3.30GHz, 1 CPU, 8 logical and 4 physical cores
  [Host]     : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT  [AttachedDebugger]
  Job-RLKOES : .NET Framework 4.8 (4.8.4515.0), X64 RyuJIT


```
|              Method |        Job |            Runtime |       Mean |     Error |   StdDev |   Gen 0 | Allocated |
|-------------------- |----------- |------------------- |-----------:|----------:|---------:|--------:|----------:|
| ExchangeRedisGetSet | Job-MNUBEA |      .NET Core 3.1 |         NA |        NA |       NA |       - |         - |
|     FreeRedisGetSet | Job-MNUBEA |      .NET Core 3.1 |         NA |        NA |       NA |       - |         - |
|         CacheGetSet | Job-MNUBEA |      .NET Core 3.1 |         NA |        NA |       NA |       - |         - |
| ExchangeRedisGetSet | Job-RLKOES | .NET Framework 4.8 | 1,953.5 μs | 130.03 μs | 368.9 μs |       - |  32,768 B |
|     FreeRedisGetSet | Job-RLKOES | .NET Framework 4.8 | 1,890.0 μs | 116.70 μs | 336.7 μs |       - |  90,160 B |
|         CacheGetSet | Job-RLKOES | .NET Framework 4.8 | 1,975.5 μs |  53.00 μs | 139.6 μs | 13.6719 |  88,496 B |

Benchmarks with issues:
  MulitClientTest.ExchangeRedisGetSet: Job-MNUBEA(Runtime=.NET Core 3.1)
  MulitClientTest.FreeRedisGetSet: Job-MNUBEA(Runtime=.NET Core 3.1)
  MulitClientTest.CacheGetSet: Job-MNUBEA(Runtime=.NET Core 3.1)
