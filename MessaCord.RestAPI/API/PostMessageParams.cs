using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MessaCord.RestAPI.API
{
    public class PostMessageParams
    {
        public PostMessageParams(string content, bool tts)
        {
            Content = content;
            Tts = tts;
        }

        public PostMessageParams(string content, bool tts, Embed embed) : this(content, tts)
        {
            Embed = embed;
        }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("embed")]
        public Embed Embed { get; set; }
    }
}
