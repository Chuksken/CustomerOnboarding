using CustomerOnb.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.Services
{
    public interface IRapidAPI
    {
        Task<BaseResponse> GetRapidPrices();
    }

    public class RapidAPI : IRapidAPI
    {
        private readonly ILogService _logService;
        private readonly IConfiguration configuration;
        private readonly AppSettings _appSettings;
        private readonly RapidHttpClient _rapidHttpClient;

        public RapidAPI(ILogService logService, IOptions<AppSettings> appSetting, RapidHttpClient rapidHttpClient)
        {
            _logService = logService;
            _appSettings = appSetting.Value;
            _rapidHttpClient = rapidHttpClient;

        }
        public async Task<BaseResponse> GetRapidPrices()
        {
            var response = new BaseResponse();
            //response.Data = new GetBillersResp();
            try
            {


                //var httpClient = _rapidHttpClient.Client;
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Host", _appSettings.RapidHost);
                //httpClient.DefaultRequestHeaders.TryAddWithoutValidation("X-RapidAPI-Key", _appSettings.RapidKey);
                //var requestUri = $"{httpClient.BaseAddress}";

                //var mdaResponse = await httpClient.GetAsync(requestUri);

                var client = new RestClient(_appSettings.RapidURL);
                var request = new RestRequest();
                request.Method = Method.Get;
                request.AddHeader("X-RapidAPI-Host", _appSettings.RapidHost);
                request.AddHeader("X-RapidAPI-Key", _appSettings.RapidKey);
                RestResponse _response = client.ExecuteAsync(request).Result;

                if (_response.IsSuccessful)
                {
                    _logService.LogInfo("RapidAPI API call was successful");
                    var responseString =  _response.Content;
                    var responseJson = JsonConvert.DeserializeObject<string>(responseString);
                    response.IsSuccess = true;
                    response.ResponseMessage = "Success";
                    response.ResponseCode = "00";
                  
                    //UpdateMDACollections(responseJson.collections);
                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseCode = _response.StatusCode.ToString();
                    response.ResponseMessage = "Failed";
                    _logService.LogError("Rapid API call failed: " + _response.ErrorException);
                }
                return await Task.FromResult(response);
            }
            catch (Exception ex)
            {
                ex.LogError(_logService);
                response.IsSuccess = false;
                response.ResponseCode = "500";
                response.ResponseMessage = "Server error occurred!";
                return await Task.FromResult(response);
                // throw ex;
            }
        }

    }
}
