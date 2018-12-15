using System;
using Newtonsoft.Json;

namespace MessaCord.RestAPI.API.Common
{
    public partial class Message
    {
        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("tts")]
        public bool Tts { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("pinned")]
        public bool Pinned { get; set; }

        [JsonProperty("nonce")]
        public string Nonce { get; set; }

        [JsonProperty("mentions")]
        public object[] Mentions { get; set; }

        [JsonProperty("mention_roles")]
        public object[] MentionRoles { get; set; }

        [JsonProperty("mention_everyone")]
        public bool MentionEveryone { get; set; }

        [JsonProperty("member")]
        public Member Member { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("embeds")]
        public object[] Embeds { get; set; }

        [JsonProperty("edited_timestamp")]
        public object EditedTimestamp { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("channel_id")]
        public ulong ChannelId { get; set; }

        [JsonProperty("author")]
        public User Author { get; set; }

        [JsonProperty("attachments")]
        public object[] Attachments { get; set; }

        [JsonProperty("guild_id")]
        public string GuildId { get; set; }
    }
}
