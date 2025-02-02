namespace EndPoint.Api.Api.RequestModels.Customers;

public class AddCustomerRequestDto
{
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}