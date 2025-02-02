namespace EndPoint.Api.Api.RequestModels.OrderItems;

public class DeleteOrderItemRequestDto
{
    public string CustomerEid { get; set; }
    public string ProductEid { get; set; }
}