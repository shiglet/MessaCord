using System;
using System.Collections.Generic;
using System.Text;

namespace MessaCord.RestAPI.Entities.Channels
{
    public class RestChannel
    {
        public ulong Id { get; set; }

        public RestChannel(ulong id)
        {
            Id = id;
        }
    }
}
