using System.Net;

namespace api.Error.Exceptions
{
    public class VerifyUserEmailException : CustomException
    {
        public VerifyUserEmailException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
