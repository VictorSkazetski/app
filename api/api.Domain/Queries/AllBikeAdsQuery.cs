using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Queries
{
    public class AllBikeAdsQuery : IRequest<List<BikeAdDto>>
    {
    }
}
