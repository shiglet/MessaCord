using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class User
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("bot")]
        public bool Bot { get; set; }
    }
}