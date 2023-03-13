using api.Data;
using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Error.Exceptions;
using api.Infrastructure.Data.Repositories.Interfaces;
using MapsterMapper;
using MediatR;
using System.Text;

namespace api.Domain.Command
{
    public class CreateUserAccountCommandHandler :
        IRequestHandler<CreateUserAccountCommand, AccountDto>
    {
        private readonly IUsersRepository userRepository;
        private readonly IEmailService emailService;
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;

        public CreateUserAccountCommandHandler(
            IUsersRepository userRepository,
            IEmailService emailService,
            IApiUserManagerServices userManager,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.emailService = emailService;
            this.userManager = userManager;
            this.mapper = mapper;
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
            var userCreated = await this.userRepository.CreateAsync(new UserEntity
            {
                UserName = request.UserEmail,
                Email = request.UserEmail,
                NormalizedEmail = request.UserEmail,
                PasswordHash = request.UserPassword,
            });
            var token = await this.userManager.GenerateEmailConfirmationTokenAsync(userCreated);
            var confirmationlink =
                "https://localhost:4200/account/verify?token=" +
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(token)) +
                "&email=" +
                Convert.ToBase64String(
                    Encoding.UTF8.GetBytes(userCreated.Email));
            await this.emailService.SendEmailAsync(confirmationlink);

            return this.mapper.Map<AccountDto>(userCreated);
        }
    }
}
