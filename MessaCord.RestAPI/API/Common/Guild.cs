using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessaCord.RestAPI.API.Common
{
    public class Guild
    {
        [JsonProperty("voice_states")]
        public List<object> VoiceStates { get; set; }
        [JsonProperty("verification_level")]
        public int VerificationLevel { get; set; }
        public bool Unavailable { get; set; }
        [JsonProperty("system_channel_id")]
        public object SystemChannelId { get; set; }
        public object Splash { get; set; }
        public List<Role> Roles { get; set; }
        public string Region { get; set; }
        public List<Presence> Presences { get; set; }
        [JsonProperty("owner_id")]
        public string OwnerId { get; set; }
        public string Name { get; set; }
        [JsonProperty("mfa_level")]
        public int MfaLevel { get; set; }
        public List<Member> Members { get; set; }
        [JsonProperty("member_count")]
        public int MemberCount { get; set; }
        public bool Lazy { get; set; }
        public bool Large { get; set; }
        [JsonProperty("joined_at")]
        public DateTime JoinedAt { get; set; }
        public string Id { get; set; }
        public string Icon { get; set; }
        public List<object> Features { get; set; }
        [JsonProperty("explicit_content_filter")]
        public int ExplicitContentFilter { get; set; }
        public List<Emoji> Emojis { get; set; }
        [JsonProperty("default_message_notifications")]
        public int DefaultMessageNotifications { get; set; }
        public List<Channel> Channels { get; set; }
        [JsonProperty("application_id")]
        public object ApplicationId { get; set; }
        [JsonProperty("afk_timeout")]
        public int AfkTimeout { get; set; }
        [JsonProperty("afk_channel_id")]
        public string AfkChannelId { get; set; }
    }
}
