using Application.Handlers.OrderItems.Commands;
using Application.Interfaces;
using Application.Models.Commands.OrderItems;
using Common.Extensions;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Moq;
using System.Net;

namespace Shop.Test.Handlers.OrderItems.Commands;

public class DeleteOrderItemHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteOrderItemHandler _handler;

    public DeleteOrderItemHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new DeleteOrderItemHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenCustomerNotFound()
    {
        // Arrange
        var command = new DeleteOrderItemCommand
        {
            CustomerId = 1,
            ProductId = 1
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as CustomerEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("کاربر یافت نشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenProductNotFound()
    {
        // Arrange
        var command = new DeleteOrderItemCommand { CustomerId = 1, ProductId = 1 };
        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new CustomerEntity());
        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as ProductEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("محصولی یافت نشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenOrderNotFound()
    {
        // Arrange
        var command = new DeleteOrderItemCommand { CustomerId = 1, ProductId = 1 };
        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new CustomerEntity());
        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new ProductEntity());
        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(null as OrderEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("کاربر مورد نظر سفارش تعیین نشده ندارد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldUpdateQuantity_WhenOrderItemExists()
    {
        // Arrange
        var command = new DeleteOrderItemCommand { CustomerId = 1, ProductId = 1 };
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, Name = "Product A" };
        var order = new OrderEntity
        {
            OrderItems = [new OrderItemEntity { ProductId = product.Id, Quantity = 2, IsRemoved = false }]
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(order);
        _mockUnitOfWork.Setup(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>())).Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _mockUnitOfWork.Verify(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>()), Times.Once);
        Assert.Equal(1, order.OrderItems.First().Quantity);
    }

    [Fact]
    public async Task Handle_ShouldRemoveOrderItem_WhenQuantityIsOne()
    {
        // Arrange
        var command = new DeleteOrderItemCommand { CustomerId = 1, ProductId = 1 };
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, Name = "Product A" };
        var order = new OrderEntity
        {
            OrderItems = [new OrderItemEntity { ProductId = product.Id, Quantity = 1, IsRemoved = false }]
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(product);
        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(order);
        _mockUnitOfWork.Setup(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>())).Verifiable();

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _mockUnitOfWork.Verify(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>()), Times.Once);
        Assert.True(order.OrderItems.First().IsRemoved); // The item should be removed
    }
}