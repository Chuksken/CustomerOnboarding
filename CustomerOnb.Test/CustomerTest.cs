using CustomerOnb.API.Controllers;
using CustomerOnb.Data.Models;
using CustomerOnb.Shared.Helpers;
using CustomerOnb.Shared.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CustomerOnb.Test
{
    public class CustomerTest
    {
        #region Property
        public Mock<ICustomerService> mock = new Mock<ICustomerService>();
        #endregion

        [Fact]
        public async void CreateCustomer()
        {
            CustomerReq req = new CustomerReq();
            req.Email = "fumi@gmail.com";
            req.PhoneNo = "07031111111";
            req.StateId = 2;
            req.LgaId = 2;

            var response = new BaseResponse();
            response.IsSuccess = true;
            response.ResponseCode = "00";
            response.ResponseMessage = "Success";
            mock.Setup(p => p.CreateCust(req)).ReturnsAsync(response);
            CustomerController emp = new CustomerController(mock.Object);
            var result = await emp.CreateCust(req);
            Assert.True(response.Equals(result));
        }
        [Fact]
        public async void GetCustomers()
        {
            var response = new BaseResponse1<List<CustomerResp>>();
            response.Data = new List<CustomerResp>();
            response.IsSuccess = true;
            response.ResponseCode = "00";
            response.ResponseMessage = "Success";

            mock.Setup(p => p.GetCust("getall")).ReturnsAsync(response);
            CustomerController emp = new CustomerController(mock.Object);
            var result = await emp.GetCust("getall");
            Assert.True(response.Equals(result));
        }
    }
}
