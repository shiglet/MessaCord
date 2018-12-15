using System;
using System.Collections.Generic;
using System.Text;

namespace MessaCord.RestAPI.Entities.Channels
{
    public class RestChannel
    {
        public ulong ChannelId { get; set; }

        public RestChannel(ulong channelId)
        {
            ChannelId = channelId;
        }
    }
}
