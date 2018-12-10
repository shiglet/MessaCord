using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.Commands;

namespace MessaCord.Test
{
    public class TestModule : Module
    {
        [Command("Hello")]
        public void Hello()
        {
            Console.WriteLine("Inside Hello command");
        }

        [Command("Laugh")]
        public void Laugh()
        {
            Console.WriteLine("HAHAHAH xDDD");
        }
    }
}
