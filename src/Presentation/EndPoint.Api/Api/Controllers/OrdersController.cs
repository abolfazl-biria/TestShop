using Application.Models.Commands.Orders;
using Application.Models.Queries.Orders;
using Common.Extensions;
using EndPoint.Api.Api.RequestModels.Orders;
using EndPoint.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class OrdersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetOrdersByFilterRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new GetOrdersByFilterQuery(
            new OrderFilter(request.Page, request.PageSize, request.Pagination)
            {
                Status = request.Status,
                CustomerId = request.CustomerEid?.Decode(),
                StartInsertDateTime = request.StartInsertDateTime,
                EndInsertDateTime = request.EndInsertDateTime,
                IsRemoved = request.IsRemoved,
                ColumnName = request.ColumnName,
                OrderDir = request.OrderDir,
            })));

    [HttpGet("{eid}")]
    public async Task<IActionResult> GetById([FromRoute] string eid) =>
        this.ReturnResponse(await mediator.Send(new GetOrderByIdQuery
        {
            Id = eid.Decode(),
        }));

    [HttpPut("{customerEid}")]
    public async Task<IActionResult> Update([FromRoute] string customerEid, [FromBody] UpdateOrderRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new UpdateOrderCommand
        {
            CustomerId = customerEid.Decode(),
            Status = request.Status,
        }));
}