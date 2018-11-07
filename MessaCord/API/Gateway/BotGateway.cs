namespace MessaCord.API.Gateway
{
    public class BotGateway : Gateway
    {
        public int Shards { get; set; }
        public SessionStartLimit SessionStartLimit { get; set; }
        public override string ToString()
        {
            return base.ToString() + SessionStartLimit + Shards;
        }
    }
}