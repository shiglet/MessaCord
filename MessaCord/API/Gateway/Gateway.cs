namespace MessaCord.API.Gateway
{
    public class Gateway
    {
        public string Url { get; set; }
        public override string ToString()
        {
            return Url;
        }
    }
}