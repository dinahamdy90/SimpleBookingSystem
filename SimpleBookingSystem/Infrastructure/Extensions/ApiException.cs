using System.Net;

namespace SimpleBookingSystem.Infrastructure.Extensions
{
    public class ApiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorCode { get; set; }
        public ApiException(HttpStatusCode statusCode, string errorCode)
        {
            StatusCode = statusCode;
            ErrorCode = errorCode;
        }
    }
}
