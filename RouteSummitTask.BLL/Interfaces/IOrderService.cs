using RouteSummitTask.BLL.Dtos;

namespace RouteSummitTask.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(int customerId, CreateOrderDto orderDto);
        decimal CalculateDiscountAmount(decimal productPrice, int quantity);
    }
}
