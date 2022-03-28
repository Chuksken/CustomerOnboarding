

namespace CustomerOnb.Shared.Helpers
{
   
    public class BaseResponse
    {
        public bool IsSuccess { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
   


    public class BaseResponse1
    {
        public bool IsSuccess { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
    public class BaseResponse1<T> : BaseResponse
    {
        public T Data { get; set; }
    }
}
