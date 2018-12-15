using System.Threading.Tasks;
using MessaCord.Network;
using MessaCord.RestAPI.Entities.Messages;
using MessaCord.Utilities.Log;

namespace MessaCord.Commands
{
    public class CommandHandler
    {
        private Logger Logger = new Logger(true);
        private readonly string _prefix;
        private readonly DiscordClient _client;
        private readonly CommandManager _commandManager;

        public CommandHandler(string prefix, DiscordClient client, CommandManager commandManager = null)
        {
            _client = client;
            _prefix = prefix;
            _client.MessageReceived += HandleCommand;
            _commandManager = commandManager;
        }

        private async Task HandleCommand(RestMessage message)
        {
            if (message == null) return;
            if (!(message.Content.StartsWith(_prefix))) return;

            Logger.Log($"{message.Author.Username}:  {message.Content}");

            await _commandManager.ExecuteCommandAsync(message);
        }
    }
}