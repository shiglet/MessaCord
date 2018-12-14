using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Log;

namespace MessaCord.Commands
{
    public class CommandManager
    {
        private readonly List<Module> _modules = new List<Module>();
        public async Task LoadModulesAsync(Assembly assembly)
        {
            await Task.Factory.StartNew(() =>
            {
                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Module))).ToList();
                foreach (var module in types)
                {
                    _modules.Add(((Module) Activator.CreateInstance(module)));
                }
            });
        }

        public async Task ExecuteCommandAsync(string message)
        {
            await Task.Factory.StartNew(() =>
            {
                string command = message.Split(" ").FirstOrDefault()?.Substring(1);
                foreach (var module in _modules)
                {
                    var type = module.GetType();
                    var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandAttribute)).Any());
                    var methodToCall = methods
                        .FirstOrDefault(m =>
                            ((MemberInfo) m).GetCustomAttributes()
                            .Any(c => (c as CommandAttribute)?.Command == command));
                    if (methodToCall == null) continue;
                    var instance = _modules.FirstOrDefault(m =>
                        methodToCall != null && m.GetType() == methodToCall.DeclaringType);
                    methodToCall.Invoke(instance, null);
                }
            });
        }
    }

    public class CommandHandler
    {
        private Logger Logger = new Logger(true);
        private string _prefix;
        private DiscordClient _client;
        private CommandManager _commandManager;

        public CommandHandler(string prefix, DiscordClient client, CommandManager commandManager = null)
        {
            _client = client;
            _prefix = prefix;
            _client.MessageReceived += HandleCommand;
            _commandManager = commandManager;
        }

        private async Task HandleCommand(Message message)
        {
            if (message == null) return;
            if (!(message.Content.StartsWith(_prefix))) return;

            Logger.Log($"{message.Author.Username}:  {message.Content}");

            await _commandManager.ExecuteCommandAsync(message.Content);
        }
    }
    
    public class XTestModule
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
