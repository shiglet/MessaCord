using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Network;

namespace MessaCord.Commands
{
    public class Module
    {
        protected DiscordClient DiscordClient;
        public CommandContext Context;

        protected async Task ReplyAsync(string message)
        {
            await DiscordClient.SendMessageAsync(Context.Message.Channel.Id, message);
        }
        public void SetContext(CommandContext value)
        {
            Context = value;
        }

        public Module(DiscordClient discordClient)
        {
            DiscordClient = discordClient; 
        }
    }
}
