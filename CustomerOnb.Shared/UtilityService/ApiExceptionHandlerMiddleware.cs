

using CustomerOnb.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomerOnb.Shared.UtilityService
{
 
    public class ApiExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogService _logService;
        public ApiExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, ILogService logService)
        {

            try
            {
                _logService = logService;
                await _next.Invoke(context);
            }
            catch (Exception error)
            {

                error.LogError(_logService);
                await HandleExceptionAsync(context, error);

            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string errorMessage;
            context.Response.ContentType = "application/json";

            if (exception is DomainException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var problemDetails = new BaseResponse
                {
                     ResponseCode = ex.ResponseCode,
                     ResponseMessage = ex.Message,
                    IsSuccess = false

                };
                errorMessage = JsonSerializer.Serialize(problemDetails);
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var problemDetails = new BaseResponse
                {
                    ResponseCode = context.Response.StatusCode.ToString(),
                    ResponseMessage = "Server error",
                    IsSuccess = false,
                };
                errorMessage = JsonSerializer.Serialize(problemDetails);
            }
           
            return context.Response.WriteAsync(errorMessage);
        }
    }
}
