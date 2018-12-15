using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.Network;
using MessaCord.RestAPI.Entities.Messages;

namespace MessaCord.Commands
{
    public class CommandContext
    {
        public DiscordClient Client { get; set; }

        public RestMessage Message { get; set; }

        public CommandContext(DiscordClient client, RestMessage message)
        {
            Client = client;
            Message = message;
        }
    }
}
