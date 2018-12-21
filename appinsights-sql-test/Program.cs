using System;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
using System.Threading;
using System.Data.SqlClient;
using Microsoft.ApplicationInsights.DependencyCollector;

namespace appinsights_sql_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = TelemetryConfiguration.Active;
            config.InstrumentationKey = "###";
            var module = new DependencyTrackingTelemetryModule();
            module.Initialize(config);

            var telemetryClient = new TelemetryClient();
            telemetryClient.TrackTrace("Start Benchmark");

            using(var conn = new SqlConnection("server=localhost;initial catalog=master;integrated security=SSPI"))
            {
                conn.Open();
                for(var i = 98; i <= 102; i++)
                {
                    var ts = new TimeSpan(0, 0, i);
                    using (var cmd = new SqlCommand($"waitfor delay '00:{ts.Minutes:00}:{ts.Seconds:00}'",conn))
                    {
                        client.TrackTrace($"Running SQL query for {ts.TotalSeconds} seconds");
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            Console.WriteLine("Done.");
            telemetryClient.Flush();
            // https://github.com/Microsoft/ApplicationInsights-dotnet/issues/407
            Thread.Sleep(10000);
        }
    }
}