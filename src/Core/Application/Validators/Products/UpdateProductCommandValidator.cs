using Application.Models.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .GreaterThan(0)
            .NotEmpty();

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