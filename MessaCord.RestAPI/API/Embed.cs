using Newtonsoft.Json;

namespace MessaCord.RestAPI.API
{
    public class Embed
    {
        public Embed(string title, string description)
        {
            Title = title;
            Description = description;
        }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}