namespace MessaCord.RestAPI.API
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