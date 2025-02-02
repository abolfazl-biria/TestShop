using Application.Models.Commands.Products;
using Application.Models.Queries.Products;
using Common.Extensions;
using EndPoint.Api.Api.RequestModels.Products;
using EndPoint.Api.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Api.Controllers;

[ApiController, Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetProductsByFilterRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new GetProductsByFilterQuery(
            new ProductFilter(request.Page, request.PageSize, request.Pagination)
            {
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                Name = request.Name,
                StartInsertDateTime = request.StartInsertDateTime,
                EndInsertDateTime = request.EndInsertDateTime,
                IsRemoved = request.IsRemoved,
                ColumnName = request.ColumnName,
                OrderDir = request.OrderDir,
            })));

    [HttpGet("{eid}")]
    public async Task<IActionResult> GetById([FromRoute] string eid) =>
        this.ReturnResponse(await mediator.Send(new GetProductByIdQuery
        {
            Id = eid.Decode(),
        }));

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddProductRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new AddProductCommand
        {
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Name = request.Name
        }));

    [HttpPut("{eid}")]
    public async Task<IActionResult> Update([FromRoute] string eid, [FromBody] UpdateProductRequestDto request) =>
        this.ReturnResponse(await mediator.Send(new UpdateProductCommand
        {
            Id = eid.Decode(),
            Price = request.Price,
            StockQuantity = request.StockQuantity,
            Name = request.Name
        }));

    [HttpDelete("{eid}")]
    public async Task<IActionResult> Delete([FromRoute] string eid) =>
        this.ReturnResponse(await mediator.Send(new DeleteProductCommand
        {
            Id = eid.Decode(),
        }));
}