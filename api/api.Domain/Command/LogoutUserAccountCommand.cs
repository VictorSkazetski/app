using MediatR;

namespace api.Domain.Command
{
    public class LogoutUserAccountCommand : IRequest
    {
        public UserTokensData userTokens { get; set; }

        public LogoutUserAccountCommand(UserTokensData tokens)
        {
            this.userTokens = tokens;
        }
    }
}
