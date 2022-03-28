using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.UtilityService
{
    public interface IGatewayService
    {
        Task<string> HttpClientGet(HttpClient client, string Url);
        Task<string> HttpClientPost(HttpClient client, string Url, HttpContent reqData);
    }

    public class GatewayService : IGatewayService
    {
        private readonly ILogger<GatewayService> logger;
        private HttpClient _client;
        public GatewayService()
        {

        }
        public async Task<string> HttpClientPost(HttpClient client, string Url, HttpContent reqData)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
                _client = new HttpClient(handler);
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                _client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
                request.Content = reqData;
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = _client.PostAsync(Url, reqData).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
            catch (HttpRequestException ex)
            {
                logger.LogInformation("Error occurred  - " + ex.Message + "||" + ex.InnerException);
                return ex.Message;
            }
        }

        public async Task<string> HttpClientGet(HttpClient client, string Url)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                HttpClientHandler handler = new HttpClientHandler() { UseDefaultCredentials = false };
                _client = new HttpClient(handler);
                client.BaseAddress = new Uri(Url);
                client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Add("Access-Control-Allow-Origin", "*");
                _client.DefaultRequestHeaders.Add("Content-Type", "application/json; charset=utf-8");
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Uri.EscapeUriString(client.BaseAddress.ToString()));
                //request.Content = new StringContent(jsonRes, Encoding.UTF8, "application/json");
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                response = _client.GetAsync(Url).Result;

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    return responseBody;
                }
            }
            catch (HttpRequestException ex)
            {
                logger.LogInformation("Error occurred  - " + ex.Message + "||" + ex.InnerException);
                return ex.Message;
            }
        }
    }
}
