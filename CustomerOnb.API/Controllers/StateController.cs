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
    public class StateController : ControllerBase
    {
        private readonly IStateService _cust;
        public StateController(IStateService customerService)
        {
            _cust = customerService;
        }
        [HttpPost, Route("CreateState")]
        public async Task<IActionResult> CreateState(StateReq req)
        {
            var response = await _cust.CreateState(req);
            return Ok(response);
        }

        //[HttpPost, Route("GetCustomers")]
        //public async Task<IActionResult> GetCust(PaginationReq req)
        //{
        //    var response = await _cust.GetCust(req);
        //    return Ok(response);
        //}
    }
}
