using Application.Models.Commands.Orders;
using Application.Validators.Orders;
using Domain.Entities.Orders;
using FluentAssertions;

namespace Shop.Test.Validations.Orders;

public class UpdateOrderCommandValidatorTests
{
    private readonly UpdateOrderCommandValidator _validator = new();


    [Fact]
    public void Should_Have_Validation_Error_When_CustomerId_Is_Zero()
    {
        // Arrange
        var command = new UpdateOrderCommand { CustomerId = 0, Status = OrderStatus.Completed };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "CustomerId" &&
            error.ErrorMessage == "شناسه مشتری باید عددی بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_CustomerId_Is_Empty()
    {
        // Arrange
        var command = new UpdateOrderCommand { CustomerId = 0, Status = OrderStatus.Completed };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "CustomerId" &&
            error.ErrorMessage == "شناسه مشتری الزامی است."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Status_Is_Pending()
    {
        // Arrange
        var command = new UpdateOrderCommand { CustomerId = 1, Status = OrderStatus.Pending };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Status" &&
            error.ErrorMessage == "وضعیت 'Pending' مجاز نیست."
        );
    }

    [Fact]
    public void Should_Pass_Validation_When_Valid_Input()
    {
        // Arrange
        var command = new UpdateOrderCommand { CustomerId = 1, Status = OrderStatus.Completed };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}