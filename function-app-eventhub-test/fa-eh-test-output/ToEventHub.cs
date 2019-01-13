using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

// invoke-webrequest -uri http://localhost:7071/api/toeventhub -method post
namespace fa_eh_test
{
    public static class ToEventHub
    {
        [FunctionName("ToEventHub")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log,
            [EventHub(eventHubName: "fatesteventhub", Connection = "eh-connection")] ICollector<string> outMessages)
        {
            outMessages.Add("Hello from the function app");
            return new OkResult();
        }
    }
}
