using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using Perfolizer.Horology;
using System.Globalization;


namespace S3.ConsoleCore3
{
    internal class Program
    {
        public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
         .Run(args, DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default)
             .WithSummaryStyle(new SummaryStyle(CultureInfo.InvariantCulture, printUnitsInHeader: false, SizeUnit.B, TimeUnit.Microsecond))
             .WithOptions(ConfigOptions.DisableOptimizationsValidator) 
             .AddValidator(ExecutionValidator.FailOnError)
             .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48),Job.Default.WithRuntime(CoreRuntime.Core31)));
    }
}
