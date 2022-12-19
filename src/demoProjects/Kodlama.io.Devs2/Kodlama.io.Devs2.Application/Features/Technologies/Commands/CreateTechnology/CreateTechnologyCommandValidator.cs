using FluentValidation;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Commands.CreateTechnology;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    public CreateTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ProgrammingLanguageId).NotEmpty();
    }
}
