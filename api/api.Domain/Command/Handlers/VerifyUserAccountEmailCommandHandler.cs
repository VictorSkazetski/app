using api.Domain.Interfaces;
using api.Domain.Model.Dto;
using api.Error.Exceptions;
using MapsterMapper;
using MediatR;

namespace api.Domain.Command.Handlers
{
    public class VerifyUserAccountEmailCommandHandler :
        IRequestHandler<VerifyUserAccountEmailCommand, UserEmailVerifyDto>
    {
        private readonly IApiUserManagerServices userManager;
        private readonly IMapper mapper;

        public VerifyUserAccountEmailCommandHandler(
            IApiUserManagerServices userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserEmailVerifyDto> Handle(
            VerifyUserAccountEmailCommand request, 
            CancellationToken cancellationToken)
        {
            var user = await this.userManager.FindByEmailAsync(request.UserEmail);
            var isEmailVerified = await this.userManager.ConfirmEmailAsync(user, request.Token);
            if (isEmailVerified.Succeeded)
            {
                return this.mapper.Map<UserEmailVerifyDto>(user);
            }
            else
            {
                throw new VerifyUserEmailException("Что-то пошло не так");
            }
        }
    }
}
