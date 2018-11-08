using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class User
    {
        [JsonProperty("username", NullValueHandling = NullValueHandling.Ignore)]
        public string Username { get; set; }

        [JsonProperty("id")] public string Id { get; set; }

        [JsonProperty("discriminator", NullValueHandling = NullValueHandling.Ignore)]
        public string Discriminator { get; set; }

        [JsonProperty("avatar", NullValueHandling = NullValueHandling.Ignore)]
        public string Avatar { get; set; }

        [JsonProperty("bot", NullValueHandling = NullValueHandling.Ignore)]
        public bool Bot { get; set; }
    }
}