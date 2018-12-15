using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MessaCord.Network;
using MessaCord.RestAPI.Entities.Messages;

namespace MessaCord.Commands
{
    public class CommandManager
    {
        private readonly List<Module> _modules = new List<Module>();
        private DiscordClient _client;

        public CommandManager(DiscordClient client)
        {
            _client = client;
        }

        public async Task LoadModulesAsync(Assembly assembly)
        {
            await Task.Factory.StartNew(() =>
            {
                var types = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Module))).ToList();
                foreach (var module in types)
                {
                    var args = new object[1];
                    args[0] = _client;
                    _modules.Add(((Module) Activator.CreateInstance(module,args)));
                }
            });
        }

        public async Task ExecuteCommandAsync(RestMessage message)
        {
            await Task.Factory.StartNew(() =>
            {
                string command = message.Content.Split(" ").FirstOrDefault()?.Substring(1);
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
                        m.GetType() == methodToCall.DeclaringType);
                    if (instance == null) continue;
                    instance.SetContext(new CommandContext(_client, message));
                    methodToCall.Invoke(instance, null);
                }
            });
        }
        
    }
}
