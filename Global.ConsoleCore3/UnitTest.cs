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

namespace Global.ConsoleCore3
{
    internal class UnitTest
    {
        //public static void Main(string[] args) => BenchmarkSwitcher.FromAssembly(typeof(UnitTest).Assembly)
        //.Run(args, DefaultConfig.Instance.AddDiagnoser(MemoryDiagnoser.Default)
        //    .WithSummaryStyle(new SummaryStyle(CultureInfo.InvariantCulture, printUnitsInHeader: false, SizeUnit.B, TimeUnit.Microsecond))
        //    .AddValidator(ExecutionValidator.FailOnError)
        //    .AddJob(Job.Default.WithRuntime(ClrRuntime.Net48)));

        //public static void Main(string[] args)
        //{
        //    var summary = BenchmarkRunner.Run<ClientAndSideTest>();
        //}
    }
}
