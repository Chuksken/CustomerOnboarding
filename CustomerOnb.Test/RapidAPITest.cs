using CustomerOnb.API.Controllers;
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
  public class RapidAPITest
    {
        #region Property
        public Mock<IRapidAPI> mock = new Mock<IRapidAPI>();
        #endregion

        public async void GetEmployeeDetails()
        {
            var response = new BaseResponse();
            response.IsSuccess = true;
            response.ResponseCode = "00";
            response.ResponseMessage = "Success";

            mock.Setup(p => p.GetRapidPrices()).ReturnsAsync(response);
            RapidAPIController emp = new RapidAPIController(mock.Object);
            var result = await emp.GetGoldPrices();
            Assert.True(response.Equals(result));
        }
    }
}
