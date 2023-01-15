using FluentValidation;

namespace Kodlama.io.Devs2.Application.Features.Auths.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
	public LoginCommandValidator()
	{
		RuleFor(x => x.UserForLoginDto.Email).NotEmpty().EmailAddress();
		RuleFor(x => x.UserForLoginDto.Password).NotEmpty(); //.MinimumLength(4);
	}
}
