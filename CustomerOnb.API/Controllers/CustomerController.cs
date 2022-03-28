using CustomerOnb.Data.Models;
using CustomerOnb.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerOnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _cust;
        public CustomerController(ICustomerService customerService)
        {
            _cust = customerService;
        }
        [HttpPost, Route("CreateCustomer")]
        public async Task<IActionResult> CreateCust(CustomerReq req)
        {
            var response = await _cust.CreateCust(req);
            return Ok(response);
        }

        [HttpGet, Route("GetCustomers")]
        public async Task<IActionResult> GetCust(string search)
        {
            var response = await _cust.GetCust(search);
            return Ok(response);
        }

        [HttpPost, Route("ValidateToken")]
        public async Task<IActionResult> ValidateToken(string phoneNo, string token)
        {
            var response = await _cust.TokenValidation(phoneNo, token);
            return Ok(response);
        }
    }
}
