using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Queries
{
    public class UserBikesQuery : IRequest<List<SellBikeDto>>
    {
    }
}
