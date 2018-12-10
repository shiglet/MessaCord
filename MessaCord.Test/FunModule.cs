using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.Commands;

namespace MessaCord.Test
{
    public class FunModule : Module
    {

        [Command("Fun")]
        public void Fun()
        {
            Console.WriteLine($"I'm having some fun !");
        }
        [Command("Cool")]
        public void Cool()
        {
            Console.WriteLine($"Chilllll !");
        }
    }
}
