namespace Domain.Entities.Orders;

public enum OrderStatus : byte
{
    Pending,
    Completed,
    Canceled
}