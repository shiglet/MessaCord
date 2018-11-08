using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MessaCord.API;
using MessaCord.API.Gateway;
using MessaCord.Common;
using MessaCord.Utilities.Configuration;
using MessaCord.Utilities.Log;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json;
using WebSocket4Net;

namespace MessaCord.Network
{
    public class DiscordClient
    {
        private Dictionary<string,Guild> _guilds = new Dictionary<string,Guild>();
        private HttpClient _httpclient = new HttpClient();
        private Config _config;
        private BotGatewayInfo _botGateway;
        private int? _lastSequence = null;
        private WebSocket ws;
        private int _interval = 0;
        public Logger _logger { get; set; }
        public DiscordClient(Config config)
        {
            _config = config;
            _logger = new Logger(true);
        }

        public async Task<bool> IdentifyAsync()
        {

            ConfigureHttpClient();
            var r = await _httpclient.GetAsync("api/gateway/bot");
            if (r.IsSuccessStatusCode)
            {
                _botGateway = await r.Content.ReadAsAsync<BotGatewayInfo>();
                ws = new WebSocket(_botGateway.Url);
                ws.MessageReceived += (sender, e) =>
                {
                    _logger.LogDebug("Message received" + e.Message);
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
                                _logger.LogError("Unmanaged gateway code");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Exception : " + ex.Message);
                    }

                };

                ws.Error += (sender, e) =>
                    _logger.LogError("Error: " + e.Exception);
                ws.Closed += (sender, e) =>
                    _logger.Log("Connection closed");
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
                default: 
                    _logger.Log("Other type");
                    break;
            }
        }

        private void HandleGuildCreate(NetworkFrame msg)
        {
            var guild = JsonConvert.DeserializeObject<Guild>(msg.Data.ToString());
            _guilds[guild.Id] =  guild;
            _logger.Log("GUILD_CREATE");
        }

        private void HandleReadyEvent(NetworkFrame msg)
        {
            var readyEvent =
                JsonConvert.DeserializeObject<ReadyEvent>(msg.Data.ToString());
            foreach (var guild in readyEvent.Guilds)
            {
                _guilds.Add(guild.Id,null);
            }
            _logger.Log("READY");
        }

        private void HeartBeatAckHandler(NetworkFrame msg)
        {
            _logger.Log("[ACK] HeartBreak");
        }

        private void HeartbeatEventHandler(NetworkFrame msg)
        {
            var toSend = JsonConvert.SerializeObject(new NetworkFrame(1, msg.Sequence));
            ws.Send(toSend);
            _logger.Log("HeartBreak sended : " + toSend);
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
                        _logger.Log("HeartBreak sended : " + toSend);
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
                _logger.LogDebug("Send : " + send);
            }
        }

        private void ConfigureHttpClient()
        {
            _httpclient.BaseAddress = new Uri(_config.DiscordApiUri);
            _httpclient.DefaultRequestHeaders.Accept.Clear();
            _httpclient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            _httpclient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bot", _config.Token);
        }
    }
}
