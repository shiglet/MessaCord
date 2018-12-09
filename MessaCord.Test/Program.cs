using System;
using System.Threading.Tasks;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Configuration;
using Newtonsoft.Json;

namespace MessaCord.Test
{
    class Program
    {
        Config _config = new ConfigManager().Config;
        private DiscordClient _client;
        static void Main(string[] args) => new Program().RunAsync().GetAwaiter().GetResult();

        async Task RunAsync()
        {
            _client = new DiscordClient(_config);
            bool identified = await _client.IdentifyAsync();
            if (identified == true)
                Console.WriteLine("Successfully identified !");
            await Task.Delay(-1);
        }
    }


    public class Identify
    {
        public Identify(string token, Properties properties, bool compress, int large_threshold, int[] shard, Presence presence)
        {
            this.token = token;
            this.properties = properties;
            this.compress = compress;
            this.large_threshold = large_threshold;
            this.shard = shard;
            this.presence = presence;
        }

        public string token { get; set; }
        public Properties properties { get; set; }
        public bool compress { get; set; }
        public int large_threshold { get; set; }
        public int[] shard { get; set; }
        public Presence presence { get; set; }
    }

    public class Properties
    {
        public Properties(string os, string browser, string device)
        {
            this.os = os;
            this.browser = browser;
            this.device = device;
        }
        [JsonProperty("$os")]
        public string os { get; set; }
        [JsonProperty("$browser")]
        public string browser { get; set; }
        [JsonProperty("$device")]
        public string device { get; set; }
    }

    public class Presence
    {
        public Presence(Game game, string status, int? since, bool afk)
        {
            this.game = game;
            this.status = status;
            this.since = since;
            this.afk = afk;
        }

        public Game game { get; set; }
        public string status { get; set; }
        public int? since { get; set; }
        public bool afk { get; set; }
    }

    public class SessionStartLimit
    {
        public int Total { get; set; }
        public int Remaining { get; set; }
        public int ResetAfter { get; set; }
    }
}
