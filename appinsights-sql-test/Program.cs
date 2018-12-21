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
            telemetryClient.TrackTrace("Hello World!");

            using(var conn = new SqlConnection("server=localhost;initial catalog=master;integrated security=SSPI"))
            {
                conn.Open();
                using(var cmd = new SqlCommand("select @@servername",conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Done.");
            telemetryClient.Flush();
            // https://github.com/Microsoft/ApplicationInsights-dotnet/issues/407
            Thread.Sleep(10000);
        }
    }
}
