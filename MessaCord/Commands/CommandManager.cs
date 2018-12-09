using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Log;

namespace MessaCord.Commands
{
    public class CommandManager
    {

    }

    public class CommandHandler
    {
        private Logger Logger = new Logger(true);
        private string _prefix;
        private DiscordClient _client;

        public CommandHandler(string prefix, DiscordClient client)
        {
            _client = client;
            _prefix = prefix;
        }

        private void HandleCommand(Message message)
        {
            if (message == null) return;
            if (!(message.Content.StartsWith(_prefix))) return;

            Logger.Log($"{message.Author.Username}:  {message.Content}");
        }
    }
    
    public class TestModule
    {
        [Command("Hello")]
        public void Hello()
        {
            Console.WriteLine("Inside Hello command");
        }

    }
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute : Attribute
    {
        public string Command { get; set; }

        public CommandAttribute(string command)
        {
            this.Command = command;
        }
    }
}
