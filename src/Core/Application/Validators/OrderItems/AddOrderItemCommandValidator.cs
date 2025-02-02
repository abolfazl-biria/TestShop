using Application.Models.Commands.OrderItems;
using FluentValidation;

namespace Application.Validators.OrderItems;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage("شناسه محصول باید بزرگتر از صفر باشد.")
            .NotNull().WithMessage("شناسه محصول نمی‌تواند مقدار null داشته باشد.")
            .NotEmpty().WithMessage("شناسه محصول الزامی است.");

        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("شناسه مشتری باید بزرگتر از صفر باشد.")
            .NotNull().WithMessage("شناسه مشتری نمی‌تواند مقدار null داشته باشد.")
            .NotEmpty().WithMessage("شناسه مشتری الزامی است.");
    }
}