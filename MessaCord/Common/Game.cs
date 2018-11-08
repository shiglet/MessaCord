using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class Game
    {
        public Game(string name, int type)
        {
            this.Name = name;
            this.Type = type;
        }

        public int Type { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)] public long CreatedAt { get; set; }
    }
}