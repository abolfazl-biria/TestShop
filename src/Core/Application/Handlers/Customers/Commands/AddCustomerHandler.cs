using Application.Helper;
using Application.Interfaces;
using Application.Models.Commands.Customers;
using Common.Dtos;
using Common.Extensions;
using MediatR;
using System.Net;

namespace Application.Handlers.Customers.Commands;

public class AddCustomerHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddCustomerCommand, ResultDto>
{
    public async Task<ResultDto> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
    {
        var phoneNumberExist = await unitOfWork.Customers
            .ExistsAsync(x => x.PhoneNumber.Trim() == request.PhoneNumber.Trim());

        if (phoneNumberExist)
            throw new AppException(HttpStatusCode.BadRequest, "شماره موبایل از قبل موجود میباشد");

        var entity = request.Create();

        unitOfWork.Customers.Add(entity);
        await unitOfWork.SaveChangesAsync();

        return new ResultDto();
    }
}