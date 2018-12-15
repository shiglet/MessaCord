using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.RestAPI.API.Common;

namespace MessaCord.API.Gateway
{
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
}
