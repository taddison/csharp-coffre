using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace logger_benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<CallVsNoCall>();

        }
    }

    [MemoryDiagnoser]
    public class CallVsNoCall
    {
        private ILogger _logger;
        private static string A_STRING = "a string";
        private static int A_INTEGER = 5;
        private static DateTime A_DATE = DateTime.UtcNow;

        public CallVsNoCall()
        {
            _logger = new NullLogger();
        }

        [Benchmark]
        public void ErrorInterpolate() => _logger.Log(LogLevel.Error, $"Message with {A_STRING} and {A_INTEGER} and {A_DATE}");
        [Benchmark]
        public void VerboseInterpolate() => _logger.Log(LogLevel.Verbose, $"Message with {A_STRING} and {A_INTEGER} and {A_DATE}");
        [Benchmark]
        public void ErrorFormatMessage() => _logger.Log(LogLevel.Error, "Message with {0} and {1} and {2}", A_STRING, A_INTEGER, A_DATE);
        [Benchmark]
        public void VerboseFormatMessage() => _logger.Log(LogLevel.Verbose, "Message with {0} and {1} and {2}", A_STRING, A_INTEGER, A_DATE);
    }
}
