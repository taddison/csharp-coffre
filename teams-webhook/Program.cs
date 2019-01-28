using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;

namespace teams_webhook
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .Build();

            Console.WriteLine($"Hello {config["TargetUri"]}");
        }
    }
}
