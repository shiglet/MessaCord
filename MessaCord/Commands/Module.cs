using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Network;

namespace MessaCord.Commands
{
    public class Module
    {
        protected DiscordClient _discordClient;
        private CommandContext _commandContext;

        protected async Task ReplyAsync(string message)
        {
            await _discordClient.SendMessageAsync(_commandContext.Message.Channel.ChannelId, message);
        }
        public void SetContext(CommandContext value)
        {
            _commandContext = value;
        }

        public Module(DiscordClient discordClient)
        {
            _discordClient = discordClient; 
        }
        public Module()
        {
        }

    }
}
