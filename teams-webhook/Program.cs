using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.FileExtensions;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json;

namespace teams_webhook
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .Build();

            var card = new {
                type = "message",
                text = "a little plain"
            };
            
            var cardContent = new StringContent(JsonConvert.SerializeObject(card));

            var client = new HttpClient();
            var result = await client.PostAsync(config["TargetUri"], cardContent);
            Console.WriteLine(result);
        }
    }
}
