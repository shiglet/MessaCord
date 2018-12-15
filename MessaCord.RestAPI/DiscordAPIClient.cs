using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using MessaCord.RestAPI.API;
using MessaCord.Utilities.Configuration;
using Newtonsoft.Json;
using WebSocket4Net;

namespace MessaCord.RestAPI
{
    public class DiscordAPIClient
    {

        private readonly HttpClient _httpclient = new HttpClient();
        private Config _config;

        public DiscordAPIClient(Config config)
        {
            _config = config;
        }

        public async Task<WebSocket> ConnectToGateway()
        {
            ConfigureHttpClient();
            var r = await _httpclient.GetAsync("api/gateway/bot");
            if (r.IsSuccessStatusCode)
            {
                var botGateway = await r.Content.ReadAsAsync<BotGatewayInfo>();
                return new WebSocket(botGateway.Url);
            }

            throw new Exception("Error while retrieving Gateway information");
        }

        public async Task<bool> SendMessageAsync(string channelId, string content)
        {
            var args = JsonConvert.SerializeObject(new PostMessageParams(content, false));
            if (args == null)
                return false;
            var response = await _httpclient.PostAsync($"api/channels/{channelId}/messages", new StringContent(args, Encoding.UTF8,"application/json"));
            if (response.IsSuccessStatusCode)
                return true;
            return false;
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
