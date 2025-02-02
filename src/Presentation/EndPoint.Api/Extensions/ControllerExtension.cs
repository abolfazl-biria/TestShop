using Common.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Api.Extensions;

public static class ControllerExtension
{
    public static IActionResult ReturnResponse(this ControllerBase controller, ResultDto result)
    {
        return result.IsSuccess switch
        {
            true => controller.Ok(result),
            false => controller.BadRequest(result)
        };
    }

    public static IActionResult ReturnResponse<T>(this ControllerBase controller, ResultDto<T> result)
    {
        return result.IsSuccess switch
        {
            true => controller.Ok(result),
            false => controller.BadRequest(result)
        };
    }
}