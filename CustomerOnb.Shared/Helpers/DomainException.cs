using System;


namespace CustomerOnb.Shared.Helpers
{
 
    public class DomainException : Exception
    {
        public string ResponseCode { get; set; }
        public DomainException(string message, string responseCode) : base(message)
        {
            this.ResponseCode = responseCode;
        }
        public DomainException(string message, Exception ex) : base(message, ex) { }
    }
}
