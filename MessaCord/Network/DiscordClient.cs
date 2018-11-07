using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using MessaCord.API;
using MessaCord.API.Gateway;
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
        private HttpClient _httpclient = new HttpClient();
        private Config _config;
        private BotGateway _botGateway;
        private int? _lastSequence = null;
        private WebSocket ws;
        private int _interval = 0;
        public Logger _logger { get; set; }
        public DiscordClient(Config config)
        {
            _config = config;
            _logger = new Logger();
            _logger.Log(LogLevel.Default, "Default");
            _logger.Log(LogLevel.Error, "Error");
            _logger.Log(LogLevel.Warning, "Warning");
            _logger.Log(LogLevel.Common, "Common");
        }

        public async Task<bool> IdentifyAsync()
        {

            ConfigureHttpClient();
            var r = await _httpclient.GetAsync("api/gateway/bot");
            if (r.IsSuccessStatusCode)
            {
                _botGateway = await r.Content.ReadAsAsync<BotGateway>();
                ws = new WebSocket(_botGateway.Url);
                ws.MessageReceived += (sender, e) =>
                {
                    Console.WriteLine("Message received" + e.Message);
                    try
                    {
                        var msg = JsonConvert.DeserializeObject<NetworkFrame>(e.Message);
                        Console.WriteLine("Deserialized : " + msg);
                        string toSend = null;
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
                                Console.WriteLine("Unmanaged gateway code");
                                break;
                        }

                        Console.WriteLine("Deserialized : " + msg);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Exception : " + ex.Message);
                    }

                };

                ws.Error += (sender, e) =>
                    Console.WriteLine("Error: " + e.Exception);
                ws.Closed += (sender, e) =>
                    Console.WriteLine("Connection closed");
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
                    var readMessage =
                        JsonConvert.DeserializeObject<Ready>(msg.Data.ToString());
                    _logger.Log("READY");
                    break;
                default: 
                    _logger.Log("Other type");
                    break;
            }
        }

        private void HeartBeatAckHandler(NetworkFrame msg)
        {
            Console.WriteLine("[ACK] HeartBreak");
        }

        private void HeartbeatEventHandler(NetworkFrame msg)
        {
            var toSend = JsonConvert.SerializeObject(new NetworkFrame(1, msg.Sequence));
            ws.Send(toSend);
            Console.WriteLine("HeartBreak sended : " + toSend);
        }

        private void HelloEventHandler(NetworkFrame msg)
        {
            var helloMsg =
                JsonConvert.DeserializeObject<GatewayHello>(msg.Data.ToString());
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
                        Console.WriteLine("HeartBreak sended : " + toSend);
                    }
                });
                var send = JsonConvert.SerializeObject(new NetworkFrame(GatewayOpCode.Identify,
                        new Identify(
                            _config.Token,
                            new Properties("windows", "disco", "disco"), false, 50,
                            new int[] { 1, 10 }, new Presence(
                                new Game("Cards Against Humanity", 0), "online", null, false))),
                    Formatting.Indented);
                ws.Send(send);
                Console.WriteLine("Send : " + send);
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
