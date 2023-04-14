using api.Domain.Model.Dto;
using MediatR;

namespace api.Domain.Queries
{
    public class UserProfileQuery : IRequest<UserProfileDto>
    {
    }
}
