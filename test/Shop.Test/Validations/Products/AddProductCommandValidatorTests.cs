using Application.Models.Commands.Products;
using Application.Validators.Products;
using FluentAssertions;

namespace Shop.Test.Validations.Products;

public class AddProductCommandValidatorTests
{
    private readonly AddProductCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Validation_Error_When_StockQuantity_Is_Less_Than_Or_Equal_Zero()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 0, Price = 100, Name = "محصول جدید" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "StockQuantity" &&
            error.ErrorMessage == "مقدار موجودی باید بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Price_Is_Less_Than_Or_Equal_Zero()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 0, Name = "محصول جدید" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Price" &&
            error.ErrorMessage == "قیمت باید بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Name_Is_Less_Than_2_Characters()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 100, Name = "ا" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Name" &&
            error.ErrorMessage == "نام باید حداقل 2 کاراکتر داشته باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Name_Is_Greater_Than_255_Characters()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 100, Name = new string('a', 256) };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Name" &&
            error.ErrorMessage == "نام نباید بیشتر از 255 کاراکتر باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Name_Is_Null()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 100, Name = null };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Name" &&
            error.ErrorMessage == "نام نمی‌تواند null باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_Name_Is_Empty()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 100, Name = string.Empty };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Name" &&
            error.ErrorMessage == "نام الزامی است."
        );
    }

    [Fact]
    public void Should_Pass_Validation_When_Valid_Input()
    {
        // Arrange
        var command = new AddProductCommand { StockQuantity = 10, Price = 100, Name = "محصول جدید" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}