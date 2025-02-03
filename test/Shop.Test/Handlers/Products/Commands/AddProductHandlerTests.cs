using Application.Handlers.Products.Commands;
using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Products;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Shop.Test.Handlers.Products.Commands;

public class AddProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly AddProductHandler _handler;

    public AddProductHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new AddProductHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExists_ThrowsAppException()
    {
        // Arrange
        var command = new AddProductCommand { Name = "Test Product", Price = 1000 };

        _unitOfWorkMock.Setup(u => u.Products.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("از قبل موجود میباشد", exception.Message);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_AddsProductAndSavesChanges()
    {
        // Arrange
        var product = new ProductEntity { Name = "New Product" };
        var command = new AddProductCommand { Name = product.Name, Price = 1000 };

        _unitOfWorkMock.Setup(u => u.Products.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(false);

        _unitOfWorkMock.Setup(u => u.Products.Add(It.IsAny<ProductEntity>()))
            .Callback<ProductEntity>(p => product = p);

        _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Products.Add(It.IsAny<ProductEntity>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        Assert.IsType<ResultDto>(result);
    }
}