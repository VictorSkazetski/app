using api.Domain.Model.Dto;
using api.Infrastructure.Data.Repositories;
using MapsterMapper;
using MediatR;

namespace api.Domain.Queries.Handlers;

public class ViewUsersActionsQueryHandler :
    IRequestHandler<ViewUsersActionsQuery, UsersActionsDto>
{
    private readonly IMapper mapper;
    private readonly UserActionsHistoryRepository userActionsHistoryRepository;

    public ViewUsersActionsQueryHandler(
        IMapper mapper,
        UserActionsHistoryRepository userActionsHistoryRepository)
    {
        this.mapper = mapper;
        this.userActionsHistoryRepository = userActionsHistoryRepository;
    }

    public async Task<UsersActionsDto> Handle(ViewUsersActionsQuery request, 
        CancellationToken cancellationToken)
    {
        var usersActionsData = new UsersActionsDto()
        {
            UsersActions = new List<UserActionDto>()
        };
        var usersActions = await this.userActionsHistoryRepository.GetAllAsync();
        foreach (var userAction in usersActions)
        {
            usersActionsData.UsersActions.Add(this.mapper.Map<UserActionDto>(userAction));
        }

        return usersActionsData;
    }
}
