using FluentValidation;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Commands.DeleteOperationClaim;

public class DeleteOperationClaimCommandValidator : AbstractValidator<DeleteOperationClaimCommand>
{
	public DeleteOperationClaimCommandValidator()
	{
		RuleFor(x => x.Id).NotEmpty();
	}
}
