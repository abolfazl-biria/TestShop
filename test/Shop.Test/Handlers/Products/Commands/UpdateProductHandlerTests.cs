using Application.Handlers.Products.Commands;
using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Extensions;
using Domain.Entities.Products;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Shop.Test.Handlers.Products.Commands;

public class UpdateProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();

        _handler = new UpdateProductHandler(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductExists_ThrowsAppException()
    {
        // Arrange
        var command = new UpdateProductCommand
        {
            Id = 1,
            Name = "Existing Product"
        };

        _unitOfWorkMock.Setup(r => r.Products.GetByIdForUpdateAsync(command.Id))
            .ReturnsAsync(new ProductEntity { Id = 1, Name = "Old Product" });

        _unitOfWorkMock.Setup(r => r.Products.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(true);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AppException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, ex.Status);
        Assert.Equal("از قبل موجود میباشد", ex.Message);
    }

    [Fact]
    public async Task Handle_WhenProductNotFound_ThrowsAppException()
    {
        // Arrange
        var command = new UpdateProductCommand { Id = 999, Name = "Non-Existent Product" };

        _unitOfWorkMock.Setup(r => r.Products.GetByIdForUpdateAsync(command.Id))
            .ReturnsAsync(null as ProductEntity);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<AppException>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, ex.Status);
        Assert.Equal("یافت نشد", ex.Message);
    }

    [Fact]
    public async Task Handle_WhenProductUpdated_CommitsTransaction()
    {
        // Arrange
        var command = new UpdateProductCommand { Id = 1, Name = "Updated Product" };
        var existingProduct = new ProductEntity { Id = 1, Name = "Old Product" };

        _unitOfWorkMock.Setup(r => r.Products.GetByIdForUpdateAsync(command.Id))
            .ReturnsAsync(existingProduct);

        _unitOfWorkMock.Setup(r => r.Products.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(false);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        _unitOfWorkMock.Verify(r => r.Products.Update(It.Is<ProductEntity>(p => p.Name == command.Name)), Times.Once);
    }

    [Fact]
    public async Task Handle_WhenExceptionOccurs_RollsBackTransaction()
    {
        // Arrange
        var command = new UpdateProductCommand { Id = 1, Name = "Updated Product" };
        var existingProduct = new ProductEntity { Id = 1, Name = "Old Product" };

        _unitOfWorkMock.Setup(r => r.Products.GetByIdForUpdateAsync(command.Id))
            .ReturnsAsync(existingProduct);

        _unitOfWorkMock.Setup(r => r.Products.ExistsAsync(It.IsAny<Expression<Func<ProductEntity, bool>>>()))
            .ReturnsAsync(false);

        _unitOfWorkMock.Setup(u => u.CommitAsync()).ThrowsAsync(new Exception("Test exception"));

        // Act & Assert
        var ex = await Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        Assert.Equal("Test exception", ex.Message);
        _unitOfWorkMock.Verify(u => u.RollbackAsync(), Times.Once);
    }
}