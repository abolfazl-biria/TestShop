using Application.Models.Commands.Customers;
using FluentValidation;

namespace Application.Validators.Customers;

public class AddCustomerCommandValidator : AbstractValidator<AddCustomerCommand>
{
    public AddCustomerCommandValidator()
    {
        RuleFor(c => c.FullName)
            .NotEmpty().WithMessage("نام کامل الزامی است.")
            .MaximumLength(100).WithMessage("نام کامل نباید بیشتر از 100 کاراکتر باشد.");

        RuleFor(c => c.PhoneNumber)
            .NotEmpty().WithMessage("شماره تلفن الزامی است.")
            .Length(11).WithMessage("شماره تلفن باید دقیقاً 11 رقم باشد.")
            .Matches(@"^09\d{9}$").WithMessage("شماره تلفن همراه باید با 09 شروع شود و تنها شامل 11 رقم باشد.");
    }
}