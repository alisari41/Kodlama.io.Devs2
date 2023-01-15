using Core.Security.Dtos;
using Core.Security.Enums;
using Kodlama.io.Devs2.Application.Features.Auths.Dtos;
using Kodlama.io.Devs2.Application.Features.Auths.Rules;
using Kodlama.io.Devs2.Application.Services.AuthService;
using Kodlama.io.Devs2.Application.Services.UserServices;
using MediatR;

namespace Kodlama.io.Devs2.Application.Features.Auths.Commands.Login;

public class LoginCommand : IRequest<LoggedDto>
{
    public UserForLoginDto UserForLoginDto { get; set; }
    public string IpAddress { get; set; }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
    {
        private readonly IUserServices _userServices;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;

        public LoginCommandHandler(IUserServices userServices, IAuthService authService, AuthBusinessRules authBusinessRules)
        {
            _userServices = userServices;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userServices.GetByEmail(request.UserForLoginDto.Email);
            await _authBusinessRules.UserShouldBeExists(user);
            await _authBusinessRules.UserPasswordShouldBeMatch(user.Id, request.UserForLoginDto.Password);

            LoggedDto loggedDto = new();

            //if (user.AuthenticatorType is not AuthenticatorType.None)
            //{
            //    if (request.UserForLoginDto.AuthenticatorCode is null)
            //    {
            //        await _authService.SendAuthenticatorCode(user);
            //    }
            //}

            var createdAccessToken = await _authService.CreatedAccessToken(user);
            var createdRefreshToken = await _authService.CreatedRefreshToken(user, request.IpAddress);
            var addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            await _authService.DeleteOldRefreshToken(user.Id);

            loggedDto.AccessToken = createdAccessToken;
            loggedDto.RefreshToken = addedRefreshToken;

            return loggedDto;
        }
    }
}
