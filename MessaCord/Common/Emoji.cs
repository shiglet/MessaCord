using Newtonsoft.Json;

namespace MessaCord.Common
{
    public class Emoji
    {
        public object[] Roles { get; set; }
        [JsonProperty("require_colons")] public bool RequireColons { get; set; }
        public string Name { get; set; }
        public bool Managed { get; set; }
        public string Id { get; set; }
        public bool Animated { get; set; }
    }
}