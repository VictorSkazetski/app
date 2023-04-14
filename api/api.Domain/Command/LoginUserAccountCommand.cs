using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class LoginUserAccountCommand : IRequest<UserTokensDto>
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public LoginUserAccountCommand(string userEmail, string userPassword)
        {
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }
}
