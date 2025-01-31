using Application.Models.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.StockQuantity)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Name)
            .MinimumLength(2)
            .MaximumLength(255)
            .NotNull()
            .NotEmpty();
    }
}