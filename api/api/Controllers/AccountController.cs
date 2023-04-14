using api.App;
using api.Domain.Command;
using api.Domain.Model.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator Mediator;

        public AccountController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpPost(RouteParts.Registration)]
        public async Task<AccountDto> Create([FromBody] AccountData command)
        {
            return await this.Mediator.Send(
                new CreateUserAccountCommand(command.UserEmail, command.UserPassword));
        }

        [HttpPost(RouteParts.Login)]
        public async Task<UserTokensDto> Login([FromBody] AccountData command)
        {
            return await this.Mediator.Send(
                new LoginUserAccountCommand(command.UserEmail, command.UserPassword));
        }

        [HttpPost(RouteParts.Refresh)]
        public async Task<UserTokensDto> Refresh([FromBody] UserTokensData command)
        {
            return await this.Mediator.Send(
                new RefreshUserTokensAccountCommand(command));
        }

        [HttpPost(RouteParts.Verify)]
        public async Task<UserEmailVerifyDto> VerifyUserEmail(
            [FromBody] EmailVerifyTokenData command)
        {
            return await this.Mediator.Send(
                new VerifyUserAccountEmailCommand(command.UserEmail, command.Token));
        }

        [Authorize]
        [HttpPost(RouteParts.Logout)]
        public async Task<IActionResult> Logout([FromBody] UserTokensData command)
        {
            await this.Mediator.Send(
                new LogoutUserAccountCommand(command));

            return Ok();
        }
    }
}
