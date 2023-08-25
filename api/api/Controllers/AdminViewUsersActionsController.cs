using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.App;
using api.Domain.Queries;
using MediatR;
using api.Domain.Model.Dto;

namespace api.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
public class AdminViewUsersActionsController : ControllerBase
{
    private readonly IMediator Mediator;

    public AdminViewUsersActionsController(IMediator mediator)
    {
        this.Mediator = mediator;
    }

    [HttpGet(RouteParts.AdminViewUsersAction)]
    public async Task<UsersActionsDto> GetUsersActions()
    {
        return await this.Mediator.Send(
                new ViewUsersActionsQuery());
    }
}
