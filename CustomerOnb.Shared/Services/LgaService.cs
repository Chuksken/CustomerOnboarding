using CustomerOnb.Data.Entities;
using CustomerOnb.Data.Models;
using CustomerOnb.Shared.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.Services
{
    public interface ILgaService
    {
        Task<BaseResponse> CreateLga(LgaReq Req);
    }

    public class LgaService : ILgaService
    {
        #region Property
        private readonly CustomerOnbdbContext _appDbContext;
        private readonly ILogService _logService;
        private readonly IConfiguration configuration;
        private readonly AppSettings _appSettings;
        #endregion


        #region Constructor
        public LgaService(CustomerOnbdbContext appDbContext, ILogService logService, IOptions<AppSettings> appSetting)
        {
            _appDbContext = appDbContext;
            _logService = logService;
            _appSettings = appSetting.Value;
        }
        #endregion
        public async Task<BaseResponse> CreateLga(LgaReq Req)
        {
            var response = new BaseResponse();
            // response.Data = new StateResp();
            try
            {

                Lga lga = new Lga();
                lga.Created = DateTime.Now;
                lga.Name = Req.Name;
                lga.StateId = Req.StateId;
                _appDbContext.Lga.Add(lga);
                _appDbContext.SaveChanges();

                response.IsSuccess = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Success";
                _logService.LogError("LGA was Created successfully!");
                return await Task.FromResult(response);

            }
            catch (Exception ex)
            {
                ex.LogError(_logService);
                response.IsSuccess = false;
                response.ResponseCode = "500";
                response.ResponseMessage = "Server error occurred! " + ex.Message;
                return await Task.FromResult(response);
                throw ex;
            }

        }
    }
}
