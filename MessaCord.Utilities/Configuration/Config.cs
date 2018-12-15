using Newtonsoft.Json;

namespace MessaCord.Utilities.Configuration
{
    public class Config
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("discord_api_uri")]
        public string DiscordApiUri { get; set; }
        [JsonProperty("prefix")]
        public string Prefix { get; set; }
    }

}
