using Application.Handlers.Products.Commands;
using Application.Interfaces;
using Application.Models.Commands.Products;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Products;
using Moq;
using System.Net;

namespace Shop.Test.Handlers.Products.Commands;

public class DeleteProductHandlerTests
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly DeleteProductHandle _handler;

    public DeleteProductHandlerTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteProductHandle(_unitOfWorkMock.Object);
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ThrowsAppException()
    {
        // Arrange
        var command = new DeleteProductCommand { Id = 1 };
        _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(null as ProductEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
    }

    [Fact]
    public async Task Handle_WhenProductExists_UpdatesAndCommits()
    {
        // Arrange
        var product = new ProductEntity { Id = 1, IsRemoved = false };
        var command = new DeleteProductCommand { Id = product.Id };

        _unitOfWorkMock.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(product);

        _unitOfWorkMock.Setup(u => u.BeginTransactionAsync()).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.CommitAsync()).Returns(Task.CompletedTask);
        _unitOfWorkMock.Setup(u => u.RollbackAsync()).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.Products.Update(It.IsAny<ProductEntity>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
        Assert.True(product.IsRemoved);
        Assert.IsType<ResultDto>(result);
    }
}
