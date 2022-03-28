using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.UtilityService
{
    public class BadRequestActionResult : IActionResult
    {
        private readonly string _errorMessage;
      
        public BadRequestActionResult(string errorMessage)
        {
            _errorMessage = errorMessage;
           
        }
        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Title = "Error Occured",
                Type = "Domain Error",
                Detail = _errorMessage,             
                Status = (int)HttpStatusCode.BadRequest,
                Instance = context.HttpContext.Request.Path

            };

           
            var errorString = JsonSerializer.Serialize(problemDetails);
            context.HttpContext.Response.WriteAsync(errorString);
            return Task.CompletedTask;
        }
    }
}
