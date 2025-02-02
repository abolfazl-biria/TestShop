using Application.Helper;
using Application.Interfaces;
using Application.Models.Commands.OrderItems;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.OrderItems.Commands;

public class AddOrderItemHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddOrderItemCommand, ResultDto>
{
    public async Task<ResultDto> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        var customer = await unitOfWork.Customers.GetByIdAsync(request.CustomerId) ??
                       throw new AppException(HttpStatusCode.BadRequest, "کاربر یافت نشد");

        var product = await unitOfWork.Products.GetByIdAsync(request.ProductId) ??
                      throw new AppException(HttpStatusCode.BadRequest, "محصولی یافت نشد");

        if (product.StockQuantity <= 0)
            throw new AppException(HttpStatusCode.BadRequest, "محصول مورد نظر در انبار موجود نمیباشد");

        var order = await unitOfWork.Orders.GetByCustomerIdAsync(customer.Id);

        if (order == null)
        {
            order = OrderHelper.Create(customer.Id, product.Id);

            unitOfWork.Orders.Add(order);
            await unitOfWork.SaveChangesAsync();

            return new ResultDto();
        }

        var orderItem = order.OrderItems.FirstOrDefault(x => !x.IsRemoved && x.ProductId == request.ProductId);
        if (orderItem != null)
        {
            if (orderItem.Quantity >= orderItem.Product.StockQuantity)
                throw new AppException(HttpStatusCode.BadGateway,
                    $"از محصول '{orderItem.Product.Name}' فقط '{orderItem.Product.StockQuantity}' عدد باقی مانده است");

            orderItem.Quantity++;
            orderItem.ModifiedDate = DateTimeOffset.UtcNow;

            unitOfWork.OrderItems.Update(orderItem);
        }
        else
        {
            var entity = OrderItemHelper.Create(product.Id, order.Id);

            unitOfWork.OrderItems.Add(entity);
        }

        await unitOfWork.SaveChangesAsync();

        return new ResultDto();
    }
}