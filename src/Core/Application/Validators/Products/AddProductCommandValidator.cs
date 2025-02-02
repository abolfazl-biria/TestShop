using Application.Models.Commands.Products;
using FluentValidation;

namespace Application.Validators.Products;

public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
{
    public AddProductCommandValidator()
    {
        RuleFor(x => x.StockQuantity)
            .GreaterThan(0).WithMessage("مقدار موجودی باید بزرگتر از صفر باشد.")
            .NotNull().WithMessage("مقدار موجودی نمی‌تواند null باشد.")
            .NotEmpty().WithMessage("مقدار موجودی الزامی است.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("قیمت باید بزرگتر از صفر باشد.")
            .NotNull().WithMessage("قیمت نمی‌تواند null باشد.")
            .NotEmpty().WithMessage("قیمت الزامی است.");

        RuleFor(x => x.Name)
            .MinimumLength(2).WithMessage("نام باید حداقل 2 کاراکتر داشته باشد.")
            .MaximumLength(255).WithMessage("نام نباید بیشتر از 255 کاراکتر باشد.")
            .NotNull().WithMessage("نام نمی‌تواند null باشد.")
            .NotEmpty().WithMessage("نام الزامی است.");

    }
}