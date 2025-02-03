using Application.Handlers.OrderItems.Commands;
using Application.Interfaces;
using Application.Models.Commands.OrderItems;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Customers;
using Domain.Entities.Orders;
using Domain.Entities.Products;
using Moq;
using System.Net;

namespace Shop.Test.Handlers.OrderItems.Commands;

public class AddOrderItemHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly AddOrderItemHandler _handler;

    public AddOrderItemHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new AddOrderItemHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenCustomerNotFound()
    {
        // Arrange
        var command = new AddOrderItemCommand
        {
            CustomerId = 1,
            ProductId = 1
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.GetByIdAsync(command.CustomerId))
            .ReturnsAsync(null as CustomerEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("کاربر یافت نشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenProductNotFound()
    {
        // Arrange
        var command = new AddOrderItemCommand
        {
            CustomerId = 1,
            ProductId = 1
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.GetByIdAsync(command.CustomerId))
            .ReturnsAsync(new CustomerEntity());
        _mockUnitOfWork.Setup(uow => uow.Products.GetByIdAsync(command.ProductId))
            .ReturnsAsync(null as ProductEntity);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("محصولی یافت نشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenProductOutOfStock()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, StockQuantity = 0 };

        var command = new AddOrderItemCommand
        {
            CustomerId = customer.Id,
            ProductId = product.Id
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.GetByIdAsync(command.CustomerId))
            .ReturnsAsync(customer);
        _mockUnitOfWork.Setup(uow => uow.Products.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("محصول مورد نظر در انبار موجود نمیباشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldAddNewOrder_WhenOrderNotExists()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, StockQuantity = 10 };
        var command = new AddOrderItemCommand
        {
            CustomerId = customer.Id,
            ProductId = product.Id
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.GetByIdAsync(command.CustomerId))
            .ReturnsAsync(customer);
        _mockUnitOfWork.Setup(uow => uow.Products.GetByIdAsync(command.ProductId))
            .ReturnsAsync(product);
        _mockUnitOfWork.Setup(uow => uow.Orders.GetByCustomerIdAsync(command.CustomerId))
            .ReturnsAsync(null as OrderEntity);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Orders.Add(It.IsAny<OrderEntity>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        Assert.IsType<ResultDto>(result);
    }

    [Fact]
    public async Task Handle_ShouldUpdateOrderItem_WhenProductAlreadyInOrder()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, StockQuantity = 10 };
        var request = new AddOrderItemCommand { CustomerId = customer.Id, ProductId = product.Id };

        var existingOrder = new OrderEntity
        {
            Id = 1,
            OrderItems = [new OrderItemEntity { ProductId = product.Id, Quantity = 5, Product = product }]
        };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(customer.Id))
            .ReturnsAsync(customer);

        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(customer.Id))
            .ReturnsAsync(existingOrder);

        _mockUnitOfWork.Setup(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>()))
            .Verifiable();

        _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockUnitOfWork.Verify(u => u.OrderItems.Update(It.IsAny<OrderItemEntity>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);

        var updatedOrderItem = existingOrder.OrderItems.First();
        Assert.Equal(6, updatedOrderItem.Quantity);
        Assert.IsType<ResultDto>(result);
    }

    [Fact]
    public async Task Handle_ShouldAddNewOrderItem_WhenProductNotInOrder()
    {
        // Arrange
        var customer = new CustomerEntity { Id = 1 };
        var product = new ProductEntity { Id = 1, StockQuantity = 10 };
        var request = new AddOrderItemCommand { CustomerId = customer.Id, ProductId = product.Id };

        _mockUnitOfWork.Setup(u => u.Customers.GetByIdAsync(customer.Id))
            .ReturnsAsync(customer);

        _mockUnitOfWork.Setup(u => u.Products.GetByIdAsync(product.Id))
            .ReturnsAsync(product);

        _mockUnitOfWork.Setup(u => u.Orders.GetByCustomerIdAsync(customer.Id))
            .ReturnsAsync(null as OrderEntity);

        _mockUnitOfWork.Setup(u => u.Orders.Add(It.IsAny<OrderEntity>()))
            .Verifiable();

        _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        _mockUnitOfWork.Verify(u => u.Orders.Add(It.IsAny<OrderEntity>()), Times.Once);
        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        Assert.IsType<ResultDto>(result);
    }
}