namespace BukuAPI.Services.OrderService
{
    public interface IOrderService
    {
        List<object>? GetAllOrders();
        object? GetSingleOrder(int id);
        List<object>? AddOrder(Order newOrder);
        List<object>? UpdateOrder(int id, Order editOrder);
        List<object>? DeleteOrder(int id);
    }
}
