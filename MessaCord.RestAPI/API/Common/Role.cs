namespace MessaCord.RestAPI.API.Common
{
    public class Role
    {
        public int Position { get; set; }
        public int Permissions { get; set; }
        public string Name { get; set; }
        public bool Mentionable { get; set; }
        public bool Managed { get; set; }
        public string Id { get; set; }
        public bool Hoist { get; set; }
        public int Color { get; set; }
    }
}