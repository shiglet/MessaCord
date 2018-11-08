using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class Activity
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        [JsonProperty("created_at")] public long CreatedAt { get; set; }
    }
}