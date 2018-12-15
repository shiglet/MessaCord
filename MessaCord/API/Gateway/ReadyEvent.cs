using System;
using MessaCord.RestAPI.API.Common;
using Newtonsoft.Json;

namespace MessaCord.API.Gateway
{
    class ReadyEvent
    {
        public class ReadState
        {
            [JsonProperty("id")]
            public string ChannelId { get; set; }

            [JsonProperty("mention_count")]
            public int MentionCount { get; set; }

            [JsonProperty("last_message_id")]
            public string LastMessageId { get; set; }
        }
        
        [JsonProperty("v")]
        public int Version { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }

        [JsonProperty("read_state")]
        public ReadState[] ReadStates { get; set; }

        [JsonProperty("guilds")]
        public Guild[] Guilds { get; set; }

        [JsonProperty("private_channels")]
        public object[] PrivateChannels { get; set; }

        [JsonProperty("relationships")]
        public Object[] Relationships { get; set; }
    }
}