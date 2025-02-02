using Application.Helper;
using Application.Interfaces;
using Application.Models.Commands.Orders;
using Common.Dtos;
using Common.Extensions;
using Domain.Entities.Orders;
using MediatR;
using System.Net;

namespace Application.Handlers.Orders.Commands;

public class UpdateOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateOrderCommand, ResultDto>
{
    public async Task<ResultDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var customer = await unitOfWork.Customers.GetByIdAsync(request.CustomerId) ??
                           throw new AppException(HttpStatusCode.BadRequest, "کاربر یافت نشد");

            var order = await unitOfWork.Orders.GetByCustomerIdAsync(customer.Id)
                         ?? throw new AppException(HttpStatusCode.BadRequest, "کاربر مورد نظر سفارش تعیین نشده ندارد");

            if (request.Status == OrderStatus.Canceled)
            {
                order.Update(request.Status);

                unitOfWork.Orders.Update(order);

                await unitOfWork.SaveChangesAsync();

                return new ResultDto();
            }

            var orderItems = order.OrderItems.Where(x => x is { IsRemoved: false, Product.IsRemoved: false }).ToList();

            if (!orderItems.Any())
                throw new AppException(HttpStatusCode.BadRequest, "محصولی برای سفارش انتخاب نشده است");

            var orderItemMostUpdate = new List<(int OrderItemId, decimal UnitPrice)>();

            foreach (var orderItem in orderItems)
            {
                //اعمال lock بر روی دیتا بیس 
                //به جای استفاده از lock در سطح کد
                var product = await unitOfWork.Products.GetByIdForUpdateAsync(orderItem.ProductId);

                if (product!.StockQuantity < orderItem.Quantity)
                    throw new AppException(HttpStatusCode.BadRequest,
                        $"از محصول '{product.Name}' فقط '{product.StockQuantity}' عدد باقی مانده است");

                product.StockQuantity -= orderItem.Quantity;

                unitOfWork.Products.Update(product);

                orderItemMostUpdate.Add((orderItem.Id, orderItem.Quantity * product.Price));
            }

            if (orderItemMostUpdate.Count > 0)
            {
                var update = order.OrderItems.Where(x => orderItemMostUpdate.Any(i => i.OrderItemId == x.Id))
                    .Select(x =>
                    {
                        var item = orderItemMostUpdate.First(i => i.OrderItemId == x.Id);
                        x.ModifiedDate = DateTimeOffset.UtcNow;
                        x.UnitPrice = item.UnitPrice;

                        return x;
                    }).ToList();

                unitOfWork.OrderItems.Update(update);
            }

            order.Update(orderItemMostUpdate.Sum(x => x.UnitPrice), OrderStatus.Completed);

            unitOfWork.Orders.Update(order);

            await unitOfWork.CommitAsync();

            return new ResultDto();
        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
    }
}