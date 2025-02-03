using Application.Models.Commands.Customers;
using Application.Validators.Customers;
using FluentAssertions;

namespace Shop.Test.Validations.Customers;

public class AddCustomerCommandValidatorTests
{
    private readonly AddCustomerCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Validation_Error_When_FullName_Is_Empty()
    {
        // Arrange
        var command = new AddCustomerCommand { FullName = string.Empty, PhoneNumber = "09123456789" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "FullName" &&
            error.ErrorMessage == "نام کامل الزامی است."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_PhoneNumber_Is_Empty()
    {
        // Arrange
        var command = new AddCustomerCommand { FullName = "ابوالفضل", PhoneNumber = string.Empty };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "PhoneNumber" &&
            error.ErrorMessage == "شماره تلفن الزامی است."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_PhoneNumber_Length_Is_Not_11()
    {
        // Arrange
        var command = new AddCustomerCommand { FullName = "ابوالفضل", PhoneNumber = "0912345678" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "PhoneNumber" &&
            error.ErrorMessage == "شماره تلفن باید دقیقاً 11 رقم باشد."
        );
    }

    [Fact]
    public void Should_Have_Validation_Error_When_PhoneNumber_Is_Invalid_Format()
    {
        // Arrange
        var command = new AddCustomerCommand { FullName = "ابوالفضل", PhoneNumber = "09123456abc" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "PhoneNumber" &&
            error.ErrorMessage == "شماره تلفن همراه باید با 09 شروع شود و تنها شامل 11 رقم باشد."
        );
    }

    [Fact]
    public void Should_Pass_Validation_When_Valid_Input()
    {
        // Arrange
        var command = new AddCustomerCommand { FullName = "ابوالفضل", PhoneNumber = "09123456789" };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}