using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class CreateUserAccountCommand : IRequest<AccountDto>
    {
        public string UserEmail { get; set; }

        public string UserPassword { get; set; }

        public CreateUserAccountCommand(string userEmail, string userPassword)
        {
            UserEmail = userEmail;
            UserPassword = userPassword;
        }
    }
}
