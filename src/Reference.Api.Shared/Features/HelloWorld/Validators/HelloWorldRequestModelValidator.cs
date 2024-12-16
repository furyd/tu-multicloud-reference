using FluentValidation;
using Reference.Api.Shared.Features.HelloWorld.Models;

namespace Reference.Api.Shared.Features.HelloWorld.Validators;

public class HelloWorldRequestModelValidator : AbstractValidator<HelloWorldRequestModel>
{
    public HelloWorldRequestModelValidator()
    {
        RuleFor(model => model.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            ;
    }
}
