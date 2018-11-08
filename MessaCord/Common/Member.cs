using System;
using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class Member
    {
        public User User { get; set; }
        public string[] Roles { get; set; }
        public string Nick { get; set; }
        public bool Mute { get; set; }
        [JsonProperty("joined_at")] public DateTime JoinedAt { get; set; }
        public bool Deaf { get; set; }
    }
}