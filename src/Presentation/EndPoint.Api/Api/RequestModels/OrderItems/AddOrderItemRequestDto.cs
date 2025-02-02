namespace EndPoint.Api.Api.RequestModels.OrderItems;

public class AddOrderItemRequestDto
{
    public string CustomerEid { get; set; }
    public string ProductEid { get; set; }
}