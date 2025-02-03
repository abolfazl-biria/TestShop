using Application.Handlers.Customers.Commands;
using Application.Interfaces;
using Application.Models.Commands.Customers;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Customers;
using Moq;
using System.Linq.Expressions;
using System.Net;

namespace Shop.Test.Handlers.Customers.Commands;

public class AddCustomerHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly AddCustomerHandler _handler;

    public AddCustomerHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new AddCustomerHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrowAppException_WhenPhoneNumberExists()
    {
        // Arrange
        var command = new AddCustomerCommand
        {
            FullName = "ابوالفضل بی ریا",
            PhoneNumber = "09017815959"
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.ExistsAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<AppException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(HttpStatusCode.BadRequest, exception.Status);
        Assert.Equal("شماره موبایل از قبل موجود میباشد", exception.Message);
    }

    [Fact]
    public async Task Handle_ShouldAddCustomer_WhenPhoneNumberDoesNotExist()
    {
        // Arrange
        var command = new AddCustomerCommand
        {
            FullName = "ابوالفضل بی ریا",
            PhoneNumber = "09017815959"
        };

        _mockUnitOfWork.Setup(uow => uow.Customers.ExistsAsync(It.IsAny<Expression<Func<CustomerEntity, bool>>>()))
            .ReturnsAsync(false);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockUnitOfWork.Verify(uow => uow.Customers.Add(It.IsAny<CustomerEntity>()), Times.Once);
        _mockUnitOfWork.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        Assert.IsType<ResultDto>(result);
    }
}