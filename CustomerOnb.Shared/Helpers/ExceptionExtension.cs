

using System;


namespace CustomerOnb.Shared.Helpers
{
    public static class ExceptionExtension
    {
        public static string LogError(this Exception ex, ILogService logService)
        {
            var message = ex.Message;
            if (ex.InnerException != null)
            {
                message += " | " + ex.InnerException.Message;
            }

            logService.LogError("Internal Server Error Occurred!");
            logService.LogError(message);
            logService.LogError(ex.StackTrace);
            return ex.Message;
        }
     
    }
}
