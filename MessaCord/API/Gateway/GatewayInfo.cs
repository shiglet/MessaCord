namespace MessaCord.API.Gateway
{
    public class GatewayInfo
    {
        public string Url { get; set; }
        public override string ToString()
        {
            return Url;
        }
    }
}