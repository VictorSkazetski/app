using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Error.Exceptions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace api.Domain.Command
{
    public class CreateUserAccountCommandHandler :
        IRequestHandler<CreateUserAccountCommand, AccountDto>
    {
        private readonly IEmailService emailService;
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;
        private readonly IUsersActionsLogService usersActionsLogService;

        public CreateUserAccountCommandHandler(
            IEmailService emailService,
            IApiUserManagerServices userManager,
            IMapper mapper,
            IUsersActionsLogService usersActionsLogService)
        {
            this.emailService = emailService;
            this.userManager = userManager;
            this.mapper = mapper;
            this.usersActionsLogService = usersActionsLogService;
        }

        public async Task<AccountDto> Handle(
            CreateUserAccountCommand request,
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByEmailAsync(request.UserEmail);
            if (user != default)
            {
                throw new ConflictException("Пользователь уже зарегистрирован");
            }
            var isUserCreated = await this.userManager.CreateUserAsync(new UserEntity
                {
                    UserName = request.UserEmail,
                    Email = request.UserEmail,
                },
                request.UserPassword);
            if (isUserCreated.Succeeded)
            {
                var userCreated = await this.userManager.FindByEmailAsync(request.UserEmail);
                var token = await this.userManager.GenerateEmailConfirmationTokenAsync(userCreated);
                var confirmationlink =
                    "https://localhost:4200/account/verify?token=" +
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(token)) +
                    "&email=" +
                    Convert.ToBase64String(
                        Encoding.UTF8.GetBytes(userCreated.Email));
                await this.emailService.SendEmailAsync(confirmationlink);

                this.usersActionsLogService.Log("Зарегистрировался", request.UserEmail);

                return this.mapper.Map<AccountDto>(userCreated);
            }
            else
            {
                return null;
            }
        }
    }
}
