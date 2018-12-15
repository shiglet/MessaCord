using System;
using Newtonsoft.Json;

namespace MessaCord.RestAPI.API.Common
{
    public class Channel
    {
        public int Type { get; set; }
        public string Topic { get; set; }
        public int Position { get; set; }

        [JsonProperty("permission_overwrites")]
        public PermissionOverwrites[] PermissionOverwrites { get; set; }

        public string Name { get; set; }

        [JsonProperty("last_pin_timestamp")] public DateTime LastPinTimestamp { get; set; }

        [JsonProperty("last_message_id")] public string LastMessageId { get; set; }
        public string Id { get; set; }

        [JsonProperty("user_limit")] public int UserLimit { get; set; }
        public int Bitrate { get; set; }

        [JsonProperty("parent_id")] public object ParentId { get; set; }
        public bool Nsfw { get; set; }
    }
}