using Application.Models.Commands.Products;
using Application.Validators.Products;
using FluentAssertions;

namespace Shop.Test.Validations.Products;

public class DeleteProductCommandValidatorTests
{
    private readonly DeleteProductCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Validation_Error_When_Id_Is_Less_Than_Or_Equal_Zero()
    {
        // Arrange
        var command = new DeleteProductCommand { Id = 0 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.Errors.Should().ContainSingle(error =>
            error.PropertyName == "Id" &&
            error.ErrorMessage == "شناسه باید بزرگتر از صفر باشد."
        );
    }

    [Fact]
    public void Should_Pass_Validation_When_Valid_Id()
    {
        // Arrange
        var command = new DeleteProductCommand { Id = 1 };

        // Act
        var result = _validator.Validate(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }
}