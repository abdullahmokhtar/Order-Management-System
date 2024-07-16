namespace RouteSummitTask.BLL.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<Order?> GetCustomerOrderDetailsAsync(int orderId, int? customerId);
        Task<IReadOnlyList<Order>> GetAllCustomerOrder(int? customerId);
    }
}
