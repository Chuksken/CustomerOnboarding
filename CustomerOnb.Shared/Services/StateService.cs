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
    public interface IStateService
    {
        Task<BaseResponse> CreateState(StateReq Req);
    }

    public class StateService : IStateService
    {
        #region Property
        private readonly CustomerOnbdbContext _appDbContext;
        private readonly ILogService _logService;
        private readonly IConfiguration configuration;
        private readonly AppSettings _appSettings;
        #endregion


        #region Constructor
        public StateService(CustomerOnbdbContext appDbContext, ILogService logService, IOptions<AppSettings> appSetting)
        {
            _appDbContext = appDbContext;
            _logService = logService;
            _appSettings = appSetting.Value;
        }
        #endregion
        public async Task<BaseResponse> CreateState(StateReq Req)
        {
            var response = new BaseResponse();
            
            try
            {

                State state = new State();
                state.CreatedDate = DateTime.Now;
                state.Name = Req.Name;
                _appDbContext.State.Add(state);
                _appDbContext.SaveChanges();

                response.IsSuccess = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Success";
                _logService.LogError("State was Created successfully!");
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
