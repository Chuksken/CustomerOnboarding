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
    public class RapidAPIController : ControllerBase
    {
        private readonly IRapidAPI _rapid;
        public RapidAPIController(IRapidAPI rapid)
        {
            _rapid = rapid;
        }
        [HttpGet, Route("GetGoldPrices")]
        public async Task<IActionResult> GetGoldPrices()
        {
            var response = await _rapid.GetRapidPrices();
            return Ok(response);
        }
    }
}
