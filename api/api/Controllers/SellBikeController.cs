using api.App;
using api.Domain.Command;
using api.Domain.Model.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    public class SellBikeController : ControllerBase
    {
        private readonly IMediator Mediator;

        public SellBikeController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [Authorize]
        [HttpPost(RouteParts.Bike)]
        public async Task<SellBikeDto> PostBikeAd([FromForm] SellBikeData command)
        {
            return await this.Mediator.Send(
                new SellBikeCommand(command));
        }

    }
}
