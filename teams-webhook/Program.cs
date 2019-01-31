using System;
using System.Collections.Generic;
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
        // https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/cards/cards-reference
        // https://messagecardplayground.azurewebsites.net/
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .Build();

            var content = new StringContent(GetMessageCard());
            Console.WriteLine(GetMessageCard());
            var client = new HttpClient();
            var result = await client.PostAsync(config["TargetUri"], content);
            Console.WriteLine(result);
        }

        static string GetPlainCard()
        {
            var card = new
            {
                type = "message",
                text = "a little plain"
            };

            return JsonConvert.SerializeObject(card);
        }

        static string GetMessageCard()
        {
            var card = new MessageCard()
            {
                type = "MessageCard",
                context = "https://schema.org/extensions",
                summary = "Something Broke",
                title = "Something is broken"
            };

            var osTarget = new Dictionary<string,string>();
            osTarget.Add("os", "default");
            osTarget.Add("uri", "https://www.google.com");
            var targets = new List<Dictionary<string,string>>();
            targets.Add(osTarget);

            var potentialAction = new Action()
            {
                type = "OpenUri",
                name = "Get Help",
                targets = targets
            };
            card.potentialAction = new List<Action>() { potentialAction };

            var section = new Section() 
            {   activitySubtitle = "Badness afoot",
                activityTitle = "Warning!"
            };
            section.facts = new List<Fact>()
            {
                new Fact() { name = "Incident Time", value = "Recently. **very** recently."},
                new Fact() { name = "Suggested action", value = "[Be upset](http://nooooooooooooooo.com/)"},
                new Fact() { name = "Value of some metric", value = "11"}
            };

            card.sections = new List<Section> { section };

            return JsonConvert.SerializeObject(card);
        }

        // Not yet supported in MS teams
        // https://microsoftteams.uservoice.com/forums/555103-public/suggestions/35793883-adaptive-cards-webhooks
        static string GetAdaptiveCard()
        {
            var card = new
            {
                type = "message",
                text = "a little plain",
                attachments = new [] {
                    new {
                        contentType = "application/vnd.microsoft.card.adaptive",
                        content = new {
                            type = "AdaptiveCard",
                            version = "1.0",
                            body = new[] {
                                new {
                                    type = "TextBlock",
                                    text = "Bad things happening",
                                    size = "large"
                                }
                            }
                        }
                    }
                }
            };

            return JsonConvert.SerializeObject(card);
        }

        class MessageCard
        {
            [JsonProperty("@type")]
            public string type {get;set;}
            [JsonProperty("@context")]
            public string context {get;set;}
            [JsonProperty]
            public string summary {get;set;}
            [JsonProperty]
            public string title {get;set;}
            [JsonProperty]
            public List<Section> sections {get;set;}
            [JsonProperty]
            public List<Action> potentialAction {get;set;}
        }

        class Section
        {
            [JsonProperty]
            public string activityTitle { get; set; }
            [JsonProperty]
            public string activitySubtitle { get; set; }
            [JsonProperty]
            public List<Fact> facts {get;set;}
        }

        class Fact
        {
            [JsonProperty]
            public string name {get; set;}
            [JsonProperty]
            public string value {get;set;}
        }

        class Action
        {
            [JsonProperty("@type")]
            public string type {get;set;}
            [JsonProperty]
            public string name {get;set;}
            [JsonProperty]
            public List<Dictionary<string,string>> targets {get;set;}
        }
    }
}
