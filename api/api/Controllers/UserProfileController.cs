using api.App;
using api.Domain.Command;
using api.Domain.Model.Dto;
using api.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IMediator Mediator;

        public UserProfileController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet(RouteParts.ProfileSettings)]
        public async Task<UserProfileDto> GetUserProfile()
        {
            return await this.Mediator.Send(
                new UserProfileQuery());
        }

        [HttpPut(RouteParts.ProfileSettings)]
        public async Task<UserProfileDto> SaveProfile([FromForm] UserProfileData command)
        {
            return await this.Mediator.Send(
                new UserProfileCommnand(command));
        }
    }
}
