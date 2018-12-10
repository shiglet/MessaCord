using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MessaCord.API.Gateway;
using MessaCord.Commands;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Configuration;
using Newtonsoft.Json;
using CommandAttribute = MessaCord.Commands.CommandAttribute;
using Module = MessaCord.Commands.Module;

namespace MessaCord.Test
{
    class Program
    {
        Config _config = new ConfigManager().Config;
        private DiscordClient _client;
        static void Main(string[] args) => new Program().RunAsync().GetAwaiter().GetResult();

        async Task RunAsync()
        {
//            _client = new DiscordClient(_config);
//            bool identified = await _client.IdentifyAsync();
//            if (identified == true)
//                Console.WriteLine("Successfully identified !");
            var assembly = Assembly.GetEntryAssembly();
            var modules = assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(Module)));
            List<Module> lmodules = new List<Module>();
            foreach (var module in modules)
            {
//                methods.AddRange(module.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandAttribute)).Any()));
                lmodules.Add(((Module)Activator.CreateInstance(module)));
            }

            string command = "Laugh";
            foreach (var lmodule in lmodules)
            {
                var type = lmodule.GetType();
                var methods = type.GetMethods().Where(m => m.GetCustomAttributes(typeof(CommandAttribute)).Any());
                var methodToCall = methods
                    .FirstOrDefault(m => ((MemberInfo) m).GetCustomAttributes().Any(c => (c as CommandAttribute)?.Command == command));
                if (methodToCall == null) continue;
                var instance = lmodules.FirstOrDefault( m=> methodToCall != null && m.GetType() == methodToCall.DeclaringType);
                methodToCall.Invoke(instance, null);
            }
            await Task.Delay(-1);
            
        }
    }
}
