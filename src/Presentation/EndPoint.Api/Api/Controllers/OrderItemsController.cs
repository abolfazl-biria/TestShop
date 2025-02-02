using Application.Models.Commands.OrderItems;
using Application.Models.Queries.OrderItems;
using Common.Extensions;
using EndPoint.Api.Api.RequestModels.OrderItems;
using EndPoint.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class OrderItemsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetOrderItemsByFilterRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new GetOrderItemsByFilterQuery(
            new OrderItemFilter(request.Page, request.PageSize, request.Pagination)
            {
                ProductId = request.ProductEid?.Decode(),
                OrderId = request.OrderEid?.Decode(),
                Quantity = request.Quantity,
                StartInsertDateTime = request.StartInsertDateTime,
                EndInsertDateTime = request.EndInsertDateTime,
                IsRemoved = request.IsRemoved,
                ColumnName = request.ColumnName,
                OrderDir = request.OrderDir,
            })));

    [HttpGet("{eid}")]
    public async Task<IActionResult> GetById([FromRoute] string eid) =>
        this.ReturnResponse(await mediator.Send(new GetOrderItemByIdQuery
        {
            Id = eid.Decode(),
        }));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddOrderItemRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new AddOrderItemCommand
        {
            CustomerId = request.CustomerEid.Decode(),
            ProductId = request.ProductEid.Decode(),
        }));

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteOrderItemRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new DeleteOrderItemCommand
        {
            CustomerId = request.CustomerEid.Decode(),
            ProductId = request.ProductEid.Decode(),
        }));
}