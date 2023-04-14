using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Command
{
    public class RefreshUserTokensAccountCommand : IRequest<UserTokensDto>
    {
        public UserTokensData userTokens { get; set; }

        public RefreshUserTokensAccountCommand(UserTokensData tokens)
        {
            this.userTokens = tokens;
        }
    }
}
