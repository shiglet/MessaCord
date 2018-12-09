using System;
using System.Threading.Tasks;
using MessaCord.API.Gateway;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Configuration;
using Newtonsoft.Json;

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
            bool identified = await _client.IdentifyAsync();
            if (identified == true)
                Console.WriteLine("Successfully identified !");
            await Task.Delay(-1);
        }
    }
}
