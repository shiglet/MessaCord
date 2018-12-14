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
            _client = new DiscordClient(_config);
            var cmdManager = new CommandManager();
            await cmdManager.LoadModulesAsync(Assembly.GetEntryAssembly());
            CommandHandler cmdHandler = new CommandHandler("!",_client, cmdManager);
            bool identified = await _client.IdentifyAsync();
            if (identified == true)
                Console.WriteLine("Successfully identified !");

            await Task.Delay(-1);
            
        }
    }
}
