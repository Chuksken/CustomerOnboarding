using CustomerOnb.Data.Entities;
using CustomerOnb.Data.Models;
using CustomerOnb.Shared.Helpers;
using CustomerOnb.Shared.UtilityService;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.Services
{
    public interface ICustomerService
    {
        Task<BaseResponse> CreateCust(CustomerReq Req);
        Task<BaseResponse1<List<CustomerResp>>> GetCust(string search);
        Task<BaseResponse> TokenValidation(string phoneNo, String Token);
       
    }

    public class CustomerService : ICustomerService
    {
        #region Property
        private readonly CustomerOnbdbContext _appDbContext;
        private readonly ILogService _logService;
        private readonly IConfiguration configuration;
        private readonly AppSettings _appSettings;
        private readonly IEncryptionManager _encryp;
        #endregion


        #region Constructor
        public CustomerService(CustomerOnbdbContext appDbContext, ILogService logService, IOptions<AppSettings> appSetting, IEncryptionManager encryp)
        {
            _appDbContext = appDbContext;
            _logService = logService;
            _appSettings = appSetting.Value;
            _encryp = encryp;
        }
        #endregion
        public async Task<BaseResponse> CreateCust(CustomerReq Req)
        {
            var response = new BaseResponse();
            //response.Data = new StateResp();
            try
            {

                var _encyptedPassword = _encryp.EncryptString(_appSettings.key, Req.Password);

                Customer customer = new Customer();
                customer.CreatedDate = DateTime.Now;
                customer.Email = Req.Email;
                customer.LgaId = Req.LgaId;
                customer.Password = _encyptedPassword;
                customer.PhoneNo = Req.PhoneNo;
                customer.StateId = Req.StateId;
                customer.IsActive = false;
                _appDbContext.Customer.Add(customer);
                _appDbContext.SaveChanges();

                response.IsSuccess = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Success";
                

                //generate and send token
                await this.sendToken(Req.PhoneNo);
                _logService.LogError("Customer was Created successfully!");
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

        public async Task<BaseResponse1<List<CustomerResp>>> GetCust(string search)
        {

            var response = new BaseResponse1<List<CustomerResp>>();
            response.Data = new List<CustomerResp>();
            List<CustomerResp> custs = new List<CustomerResp>();
            List<Customer> _resp = null;
            try
            {

                 if(search.ToUpper() == "GETALL")
                 {
                    _resp = _appDbContext.Customer.AsQueryable().ToList();
                 }
                 else
                 {
                    _resp = _appDbContext.Customer.Where(x => x.Email == search || x.PhoneNo == search).ToList();
                }
               
               
                foreach (var item in _resp)
                {
                    CustomerResp customerResp = new CustomerResp();
                    customerResp.Email = item.Email;
                    customerResp.Lga = _appDbContext.Lga.Where(x => x.Id == item.LgaId).FirstOrDefault().Name;
                    customerResp.State = _appDbContext.State.Where(x => x.Id == item.StateId).FirstOrDefault().Name;
                    customerResp.PhoneNo = item.PhoneNo;
                    custs.Add(customerResp);
                }

                response.IsSuccess = true;
                response.ResponseCode = "00";
                response.ResponseMessage = "Success";
                response.Data = custs;
                _logService.LogError("Customer was Created successfully!");
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

       
        public async Task<BaseResponse> TokenValidation(string phoneNo, String Token)
        {
            DateTime _datenow = DateTime.Now;
            var response = new BaseResponse();
            try
            {
                var _validate = _appDbContext.TokenTable.Where(x => x.PhoneNo == phoneNo && x.Token == Token).FirstOrDefault();
                
                if (_validate == null)
                {
                    response.IsSuccess = false;
                    response.ResponseCode = "05";
                    response.ResponseMessage = "Record does not exist";
                    return await Task.FromResult(response);
                }


                if (_datenow <= _validate.TokenExpiration && _validate.Token == Token && _validate.IsUsed == false)
                {
                    var _user = _appDbContext.Customer.Where(x => x.PhoneNo == phoneNo).FirstOrDefault();
                    response.IsSuccess = false;
                    response.ResponseCode = "00";
                    response.ResponseMessage = "Success";
                    _validate.IsUsed = true;
                    _user.IsActive = true;
                    _appDbContext.SaveChanges();
                    return await Task.FromResult(response);

                }
                else
                {
                    response.IsSuccess = false;
                    response.ResponseCode = "07";
                    response.ResponseMessage = "Token has been Expired/used, please try again";
                    return await Task.FromResult(response);
                }

            }
            catch (Exception ex)
            {
                ex.LogError(_logService);
                response.IsSuccess = false;
                response.ResponseCode = "06";
                response.ResponseMessage = ex.Message;
                return await Task.FromResult(response);
                throw ex;

            }


        }
       
        private async Task sendToken(string phoneNo)
        {

            int tokenexpiration = Int32.Parse(_appSettings.tokenexpiration);


            try
            {

                var random = new Random();
                string _Newtoken = random.Next(100000, 999999).ToString();
                string _body = $"Dear {phoneNo}," + Environment.NewLine + Environment.NewLine + $"This is your Token Code: {_Newtoken}. Thank you.";


                TokenTable _token = new TokenTable()
                {
                    PhoneNo = phoneNo,
                    IsUsed = false,
                    Token = _Newtoken,
                    TokenGeneratedTime = DateTime.Now,
                    TokenExpiration = DateTime.Now.AddMinutes(tokenexpiration)
                };
                _appDbContext.TokenTable.Add(_token);
                _appDbContext.SaveChanges();

                // send an sms to user


            }
            catch (Exception e)
            {
                e.LogError(_logService);

            }


        }

       

    }
}
