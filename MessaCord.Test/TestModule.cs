using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Commands;
using MessaCord.Network;

namespace MessaCord.Test
{
    public class TestModule : Module
    {
        public TestModule(DiscordClient discordClient) : base(discordClient)
        {
        }

        [Command("Hello")]
        public async Task Hello()
        {
            await ReplyAsync("Inside Hello command");
        }

        [Command("Laugh")]
        public async Task Laugh()
        {
            await ReplyAsync("HAHAHAH xDDD");
        }
    }
}
