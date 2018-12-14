using System.Threading.Tasks;
using MessaCord.Common;
using MessaCord.Network;
using MessaCord.Utilities.Log;

namespace MessaCord.Commands
{
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
}