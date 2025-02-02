using Application.Interfaces;
using Application.Models.Commands.OrderItems;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.OrderItems.Commands;

public class DeleteOrderItemHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteOrderItemCommand, ResultDto>
{
    public async Task<ResultDto> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.CustomerId) ??
                       throw new AppException(HttpStatusCode.BadRequest, "کاربر یافت نشد");

        var product = await unitOfWork.Products.GetByIdAsync(request.ProductId) ??
                      throw new AppException(HttpStatusCode.BadRequest, "محصولی یافت نشد");

        var order = await unitOfWork.Orders.GetByCustomerIdAsync(customer.Id) ??
                    throw new AppException(HttpStatusCode.BadRequest, "کاربر مورد نظر سفارش تعیین نشده ندارد");

        var orderItem = order.OrderItems.FirstOrDefault(x => !x.IsRemoved && x.ProductId == product.Id) ??
                        throw new AppException(HttpStatusCode.BadRequest, $"کاربر محصول '{product.Name}' را ندارد");

        if (orderItem.Quantity > 1)
        {
            orderItem.Quantity--;
            orderItem.ModifiedDate = DateTimeOffset.UtcNow;
        }
        else
        {
            orderItem.IsRemoved = true;
            orderItem.ModifiedDate = DateTimeOffset.UtcNow;
        }

        unitOfWork.OrderItems.Update(orderItem);

        await unitOfWork.SaveChangesAsync();

        return new ResultDto();
    }
}