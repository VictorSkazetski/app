using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class VerifyUserAccountEmailCommand : IRequest<UserEmailVerifyDto>
    {
        public string UserEmail { get; set; }

        public string Token { get; set; }

        public VerifyUserAccountEmailCommand(string userEmail, string token)
        {
            UserEmail = userEmail;
            Token = token;
        }
    }
}
