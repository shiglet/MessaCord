using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MessaCord.API.Gateway
{
    public class Properties
    {
        public Properties(string os, string browser, string device)
        {
            this.os = os;
            this.browser = browser;
            this.device = device;
        }
        [JsonProperty("$os")]
        public string os { get; set; }
        [JsonProperty("$browser")]
        public string browser { get; set; }
        [JsonProperty("$device")]
        public string device { get; set; }
    }
}
