using System.Net;

namespace api.Error.Exceptions
{
    public class CustomException : System.Exception
    {
        public string Message { get; }

        public HttpStatusCode Status { get; }

        public CustomException(
            string message,
            HttpStatusCode status = HttpStatusCode.InternalServerError)
                : base(message)
        {
            Message = message;
            Status = status;
        }
    }
}
