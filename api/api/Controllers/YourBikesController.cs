using api.App;
using api.Domain.Command;
using api.Domain.Model.Dto;
using api.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    public class YourBikesController : ControllerBase
    {
        private readonly IMediator Mediator;

        public YourBikesController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet(RouteParts.YourBikes)]
        public async Task<List<SellBikeDto>> UserBikes()
        {
            return await this.Mediator.Send(
                new UserBikesQuery());
        }

        [HttpDelete(RouteParts.DeleteYourBikes)]
        public async Task<IActionResult> DeleteBikeAd(int bikeId)
        {
            var result = await this.Mediator.Send(
                new DeleteBikeCommand(bikeId));

            return result ? Ok() : BadRequest();
        }
    }
}
