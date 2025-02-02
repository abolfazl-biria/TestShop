using Application.Models.Commands.Customers;
using Application.Models.Queries.Customers;
using Common.Extensions;
using EndPoint.Api.Api.RequestModels.Customers;
using EndPoint.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class CustomersController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetCustomersByFilterRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new GetCustomersByFilterQuery(
            new CustomerFilter(request.Page, request.PageSize, request.Pagination)
            {
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                StartInsertDateTime = request.StartInsertDateTime,
                EndInsertDateTime = request.EndInsertDateTime,
                IsRemoved = request.IsRemoved,
                ColumnName = request.ColumnName,
                OrderDir = request.OrderDir,
            })));

    [HttpGet("{eid}")]
    public async Task<IActionResult> GetById([FromRoute] string eid) =>
        this.ReturnResponse(await mediator.Send(new GetCustomerByIdQuery
        {
            Id = eid.Decode(),
        }));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCustomerRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new AddCustomerCommand
        {
            PhoneNumber = request.PhoneNumber,
            FullName = request.FullName,
        }));
}