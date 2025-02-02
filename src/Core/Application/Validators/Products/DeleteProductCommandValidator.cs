using Application.Models.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotNull().WithMessage("شناسه نمی‌تواند null باشد.")
            .GreaterThan(0).WithMessage("شناسه باید بزرگتر از صفر باشد.")
            .NotEmpty().WithMessage("شناسه الزامی است.");
    }
}