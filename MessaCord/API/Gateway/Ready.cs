using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MessaCord.API.Gateway
{
    class Ready
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
        public object[] Guilds { get; set; }

        [JsonProperty("private_channels")]
        public object[] PrivateChannels { get; set; }

        [JsonProperty("relationships")]
        public Object[] Relationships { get; set; }
    }

    public class User
    {
        public bool Verified { get; set; }

        public string Username { get; set; }

        [JsonProperty("mfa_enabled")]
        public bool MfaEnabled { get; set; }

        public string Id { get; set; }

        public object Email { get; set; }

        public string Discriminator { get; set; }

        public bool Bot { get; set; }

        public string Avatar { get; set; }
    }
}
