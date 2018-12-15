namespace MessaCord.RestAPI.API.Common
{
    public class PermissionOverwrites
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public int Deny { get; set; }
        public int Allow { get; set; }
    }
}