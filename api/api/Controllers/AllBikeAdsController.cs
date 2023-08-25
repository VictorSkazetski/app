using System.Data;
using api.App;
using api.Domain.Model.Dto;
using api.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    public class AllBikeAdsController : ControllerBase
    {
        private readonly IMediator Mediator;

        public AllBikeAdsController(IMediator mediator)
        {
            this.Mediator = mediator;
        }

        [HttpGet(RouteParts.Bikes)]
        public async Task<List<BikeAdDto>> GetAllBikesAds()
        {
            return await this.Mediator.Send(
                new AllBikeAdsQuery());
        }

        [HttpGet(RouteParts.BikeById)]
        public async Task<BikeAdDto> GetBikeAdById(int bikeId)
        {
            return await this.Mediator.Send(
                new BikeAdByIdQuery(bikeId));
        }
    }
}
