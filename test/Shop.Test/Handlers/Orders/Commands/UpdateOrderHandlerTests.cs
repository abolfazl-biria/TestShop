using Application.Handlers.Orders.Commands;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Models.Commands.Orders;
using Common.Extensions;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Moq;
using System.Net;

namespace Shop.Test.Handlers.Orders.Commands;

public class UpdateOrderHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IProductRepository> _mockProductRepository;
    private readonly UpdateOrderHandler _handler;

    public UpdateOrderHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockProductRepository = new Mock<IProductRepository>();
        _mockUnitOfWork.Setup(u => u.Products).Returns(_mockProductRepository.Object);
        _handler = new UpdateOrderHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_CustomerNotFound_ThrowsException()
    {
        // Arrange
        var command = new UpdateOrderCommand
        {
            CustomerId = 1,
            Status = OrderStatus.Completed
        };
        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(null as CustomerEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("کاربر یافت نشد", exception.Message);
    }

    [Fact]
    public async Task Handle_OrderNotFound_ThrowsException()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var request = new UpdateOrderCommand
        {
            CustomerId = customer.Id,
            Status = OrderStatus.Completed
        };

        _mockUnitOfWork.Setup(c => c.Customers.GetByIdAsync(request.CustomerId)).ReturnsAsync(customer);
        _mockUnitOfWork.Setup(o => o.Orders.GetByCustomerIdAsync(customer.Id)).ReturnsAsync(null as OrderEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(request, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("کاربر مورد نظر سفارش تعیین نشده ندارد", exception.Message);
    }

    [Fact]
    public async Task Handle_OrderItemsNotFound_ShouldThrowException()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };

        var order = new OrderEntity
        {
            Id = 1,
            CustomerId = customer.Id,
            OrderItems = []
        };

        var command = new UpdateOrderCommand
        {
            CustomerId = customer.Id,
            Status = OrderStatus.Completed
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(order);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("محصولی برای سفارش انتخاب نشده است", exception.Message);
    }

    [Fact]
    public async Task Handle_ProductStockNotSufficient_ShouldThrowException()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity
        {
            Id = 1,
            StockQuantity = 5,
            Price = 100,
            Name = "Product1"
        };

        var order = new OrderEntity
        {
            Id = 1,
            CustomerId = customer.Id,
            OrderItems = [new OrderItemEntity { ProductId = product.Id, Quantity = 10, Product = product }]
        };

        var command = new UpdateOrderCommand { CustomerId = customer.Id, Status = OrderStatus.Completed };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(customer);
        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(It.IsAny<int>())).ReturnsAsync(order);
        _mockProductRepository.Setup(r => r.GetByIdForUpdateAsync(It.IsAny<int>())).ReturnsAsync(product);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("از محصول 'Product1' فقط '5' عدد باقی مانده است", exception.Message);
    }

    [Fact]
    public async Task Handle_SuccessfulUpdate_ShouldReturnResultDto()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, Name = "Product1", StockQuantity = 10, Price = 100 };
        var order = new OrderEntity
        {
            Id = 1,
            CustomerId = customer.Id,
            Status = OrderStatus.Pending,
            OrderItems =
            [
                new OrderItemEntity
                {
                    Id = 1,
                    ProductId = product.Id,
                    Quantity = 2,
                    UnitPrice = 100,
                    IsRemoved = false,
                    Product = product
                }
            ]
        };

        var request = new UpdateOrderCommand
        {
            CustomerId = customer.Id,
            Status = OrderStatus.Completed
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(customer.Id))
            .ReturnsAsync(customer);

        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(customer.Id))
            .ReturnsAsync(order);

        _mockUnitOfWork.Setup(u => u.Products.GetByIdForUpdateAsync(1))
            .ReturnsAsync(product);

        _mockUnitOfWork.Setup(u => u.CommitAsync())
            .Returns(Task.CompletedTask);

        _mockUnitOfWork.Setup(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>())).Verifiable();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        _mockUnitOfWork.Verify(u => u.CommitAsync(), Times.Once);
    }
}
