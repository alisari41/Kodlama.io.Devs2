using FluentValidation;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.CreateOperationClaim;

public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
{
	public CreateOperationClaimCommandValidator()
	{
		RuleFor(x=>x.Name).NotEmpty();
	}
}
