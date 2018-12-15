using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MessaCord.API;
using MessaCord.API.Gateway;
using MessaCord.RestAPI;
using MessaCord.RestAPI.API.Common;
using MessaCord.RestAPI.Entities.Channels;
using MessaCord.RestAPI.Entities.Messages;
using MessaCord.Utilities.Configuration;
using MessaCord.Utilities.Log;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using WebSocket4Net;
using Presence = MessaCord.API.Gateway.Presence;

namespace MessaCord.Network
{
    public class DiscordClient
    {
        private readonly Dictionary<string,Guild> _guilds = new Dictionary<string,Guild>();
        private readonly Config _config;
        private readonly int? _lastSequence = null;
        private WebSocket ws;
        private int _interval = 0;
        private readonly DiscordAPIClient _discordApiClient;
        public Logger Logger { get; set; } = new Logger(true);
        public Func<RestMessage,Task> MessageReceived;
        public DiscordClient(Config config)
        {
            _config = config;
            _discordApiClient = new DiscordAPIClient(_config);
        }
        public async Task<bool> StartAsync()
        {
            ws = await _discordApiClient.ConnectToGateway();
            if (ws != null)
            {
                ws.MessageReceived += (sender, e) =>
                {
                    Logger.LogDebug("Message received" + e.Message);
                    try
                    {
                        var msg = JsonConvert.DeserializeObject<NetworkFrame>(e.Message);
                        switch (msg.OperationCode)
                        {
                            case GatewayOpCode.Hello:
                                HelloEventHandler(msg);
                                break;
                            case GatewayOpCode.Heartbeat:
                                HeartbeatEventHandler(msg);
                                break;
                            case GatewayOpCode.HeartbeatAck:
                                HeartBeatAckHandler(msg);
                                break;
                            case GatewayOpCode.Dispatch:
                                DispatchHandler(msg);
                                break;
                            default:
                                Logger.LogError("Unmanaged gateway code");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError("Exception : " + ex.Message);
                    }

                };

                ws.Error += (sender, e) =>
                    Logger.LogError("Error: " + e.Exception);
                ws.Closed += (sender, e) =>
                    Logger.Log("Connection closed");
                ws.Open();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void DispatchHandler(NetworkFrame msg)
        {
            switch (msg.Type)
            {
                case "READY":
                    HandleReadyEvent(msg);
                    break;
                case "GUILD_CREATE":
                    HandleGuildCreate(msg);
                    break;
                case "MESSAGE_CREATE":
                    var message = JsonConvert.DeserializeObject<Message>(msg.Data.ToString());
                    OnMessageReceived(RestMessage.CreateMessage(message));
                    break;
                default: 
                    Logger.Log("Other type");
                    break;
            }
        }

        private void OnMessageReceived(RestMessage msg)
        {
            MessageReceived?.Invoke(msg);
        }

        private void HandleGuildCreate(NetworkFrame msg)
        {
            var guild = JsonConvert.DeserializeObject<Guild>(msg.Data.ToString());
            _guilds[guild.Id] =  guild;
            Logger.Log("GUILD_CREATE");
        }

        private void HandleReadyEvent(NetworkFrame msg)
        {
            var readyEvent =
                JsonConvert.DeserializeObject<ReadyEvent>(msg.Data.ToString());
            foreach (var guild in readyEvent.Guilds)
            {
                _guilds.Add(guild.Id,null);
            }
            Logger.Log("READY");
        }

        private void HeartBeatAckHandler(NetworkFrame msg)
        {
            Logger.Log("[ACK] HeartBreak");
        }

        private void HeartbeatEventHandler(NetworkFrame msg)
        {
            var toSend = JsonConvert.SerializeObject(new NetworkFrame(1, msg.Sequence));
            ws.Send(toSend);
            Logger.Log("HeartBreak sended : " + toSend);
        }

        private void HelloEventHandler(NetworkFrame msg)
        {
            var helloMsg =
                JsonConvert.DeserializeObject<HelloEvent>(msg.Data.ToString());
            _interval = helloMsg.HeartbeatInterval;
            if (_interval != 0)
            {
                Task.Factory.StartNew(async () =>
                {
                    while (true)
                    {
                        await Task.Delay(_interval);
                        var toSend = JsonConvert.SerializeObject(new NetworkFrame()
                        {
                            OperationCode = GatewayOpCode.Heartbeat,
                            Sequence = _lastSequence
                        });
                        ws.Send(toSend);
                        Logger.Log("HeartBreak sent : " + toSend);
                    }
                });
                var send = JsonConvert.SerializeObject(new NetworkFrame(GatewayOpCode.Identify,
                        new Identify(
                            _config.Token,
                            new Properties("windows", "disco", "disco"), false, 50,
                            new int[] { 0, 1 }, new Presence(
                                new Game("Cards Against Humanity", 0), "online", null, false))),
                    Formatting.Indented);
                ws.Send(send);
                Logger.LogDebug("Send : " + send);
            }
        }

        public async Task SendMessageAsync(ulong messageChannelId, string content)
        {
            await _discordApiClient.SendMessageAsync(messageChannelId, content);
        }
    }
}
