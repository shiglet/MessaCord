﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace MessaCord.API.Gateway
{
    public class GatewayHello
    {
        [JsonProperty("heartbeat_interval")]
        public int HeartbeatInterval { get; set; }
        public List<int> Trace { get; set; }
    }
}