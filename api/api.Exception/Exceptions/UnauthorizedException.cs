using System.Net;

namespace api.Error.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
}
