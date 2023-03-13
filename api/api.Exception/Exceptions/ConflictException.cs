using System.Net;

namespace api.Error.Exceptions
{
    public class ConflictException : CustomException
    {
        public ConflictException(string message)
            : base(message, HttpStatusCode.Conflict)
        {
        }
    }
}
