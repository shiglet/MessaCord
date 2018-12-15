using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Commands;
using MessaCord.Network;

namespace MessaCord.Test
{
    public class FunModule : Module
    {

        [Command("Fun")]
        public async Task Fun(string name)
        {
            await ReplyAsync($"I'm having some fun ! {name}");
        }
        [Command("Age")]
        public async Task Age(int age)
        {
            await ReplyAsync($"You are {age} years old");
        }
        [Command("Cool")]
        public async Task Cool()
        {
            await ReplyAsync($"Chilllll !");
        }

        public FunModule(DiscordClient d) : base(d)
        {

        }
        
    }
}
