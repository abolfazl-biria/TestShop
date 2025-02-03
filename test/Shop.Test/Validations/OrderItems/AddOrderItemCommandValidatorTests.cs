using Application.Models.Commands.OrderItems;
using Application.Validators.OrderItems;
using FluentAssertions;

namespace Shop.Test.Validations.OrderItems;

public class AddOrderItemCommandValidatorTests
{
    private readonly AddOrderItemCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Validation_Error_When_ProductId_Is_Zero()
    {
        // Arrange
        var command = new AddOrderItemCommand { ProductId = 0, CustomerId = 1 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "ProductId" &&
            error.ErrorMessage == "شناسه محصول باید بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_ProductId_Is_Empty()
    {
        // Arrange
        var command = new AddOrderItemCommand { ProductId = 0, CustomerId = 1 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "ProductId" &&
            error.ErrorMessage == "شناسه محصول الزامی است."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_CustomerId_Is_Zero()
    {
        // Arrange
        var command = new AddOrderItemCommand { ProductId = 1, CustomerId = 0 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "CustomerId" &&
            error.ErrorMessage == "شناسه مشتری باید بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_CustomerId_Is_Empty()
    {
        // Arrange
        var command = new AddOrderItemCommand { ProductId = 1, CustomerId = 0 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "CustomerId" &&
            error.ErrorMessage == "شناسه مشتری الزامی است."
        );
    }

    [Fact]
    public void Should_Pass_Validation_When_Valid_Input()
    {
        // Arrange
        var command = new AddOrderItemCommand { ProductId = 1, CustomerId = 1 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}