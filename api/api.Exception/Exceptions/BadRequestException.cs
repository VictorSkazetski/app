using System.Net;

namespace api.Error.Exceptions
{
    public class BadRequestException : CustomException
    {
        public BadRequestException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
