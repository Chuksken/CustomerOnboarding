

using Microsoft.Extensions.Options;
using System;
using System.Net.Http;

namespace CustomerOnb.Shared.Helpers
{
    public abstract class HttpClientHelper
    {
        internal AppSettings _appSettings;
        public HttpClient Client;
    }
    public class RapidHttpClient : HttpClientHelper
    {
        public RapidHttpClient(HttpClient _client, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            Client = _client;
            Client.BaseAddress = new Uri(_appSettings.RapidURL);

        }

    }
}
