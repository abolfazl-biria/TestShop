using Application.Models.Commands.Orders;
using Domain.Entities.Orders;
using FluentValidation;

namespace Application.Validators.Orders;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotNull().WithMessage("شناسه مشتری نمی‌تواند مقدار null داشته باشد.")
            .GreaterThan(0).WithMessage("شناسه مشتری باید عددی بزرگتر از صفر باشد.")
            .NotEmpty().WithMessage("شناسه مشتری الزامی است.");

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("وضعیت سفارش نامعتبر است.")
            .NotNull().WithMessage("وضعیت سفارش نمی‌تواند مقدار null داشته باشد.")
            .NotEmpty().WithMessage("وضعیت سفارش الزامی است.")
            .Must(status => status != OrderStatus.Pending)
            .WithMessage("وضعیت 'Pending' مجاز نیست.");
    }
}