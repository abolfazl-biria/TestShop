using Application.Models.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0)
            .NotEmpty();
    }
}