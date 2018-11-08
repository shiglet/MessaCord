namespace MessaCord.Common
{
    public class Presence
    {
        public User User { get; set; }
        public string Status { get; set; }
        public Game Game { get; set; }
        public Activity[] Activities { get; set; }
    }
}