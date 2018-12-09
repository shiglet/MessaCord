using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.Common;

namespace MessaCord.API.Gateway
{
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
}
