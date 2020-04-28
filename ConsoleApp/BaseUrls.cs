using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    [JsonObject("baseUrls")]
    public class BaseUrls
    {
        [JsonProperty("targetClient")]
        public string TargetClient { get; set; }
    }
}
