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
    public class LgaController : ControllerBase
    {
        private readonly ILgaService _cust;
        public LgaController(ILgaService customerService)
        {
            _cust = customerService;
        }
        [HttpPost, Route("CreateLga")]
        public async Task<IActionResult> CreateCust(LgaReq req)
        {
            var response = await _cust.CreateLga(req);
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
