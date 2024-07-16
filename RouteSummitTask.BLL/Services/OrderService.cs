

using RouteSummitTask.BLL.Dtos;

namespace RouteSummitTask.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public decimal CalculateDiscountAmount(decimal productPrice, int quantity)
        {
            decimal discount = 0, total = productPrice * quantity;
            if (total > 200)
                discount = 0.1m;
            else if (total > 100)
                discount = 0.05m;
            return total * discount;
        }

        public async Task<Order> CreateOrder(int customerId, CreateOrderDto model)
        {
            Order order = new()
            {
                CustomerId = customerId,
                PaymentMethod = model.PaymentMethod,
            };

            Dictionary<int, int> originalStocks = [];

            try
            {
                foreach (var orderItem in model.OrderItems)
                {
                    var prod = await _unitOfWork.ProductRepository.GetByIdAsync(orderItem.ProductId);
                    if (prod == null)
                        throw new Exception($"There is no product with this id = {orderItem.ProductId}");

                    if (order.OrderItems.FirstOrDefault(oi => oi.ProductId == orderItem.ProductId) != null)
                        throw new Exception("You can not add the same product more than once");

                    if (orderItem.Quantity > prod.Stock)
                        throw new Exception($"Sorry there is no enough qunatity in stock to provide this quantity the maximum qunatity for {prod.Name} is {prod.Stock}");

                    if (!originalStocks.ContainsKey(orderItem.ProductId))
                        originalStocks[prod.Id] = prod.Stock;

                    OrderItem newOrderItem = new()
                    {
                        Discount = CalculateDiscountAmount(prod.Price, orderItem.Quantity),
                        ProductId = prod.Id,
                        Quantity = orderItem.Quantity,
                        UnitPrice = prod.Price
                    };

                    order.OrderItems.Add(newOrderItem);

                    prod.Stock -= orderItem.Quantity;
                    await _unitOfWork.CompleteAsync();
                    order.TotalAmount += newOrderItem.UnitPrice * newOrderItem.Quantity - newOrderItem.Discount;
                }
            }
            catch (Exception ex)
            {
                foreach (var kvp in originalStocks)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(kvp.Key);
                    product.Stock = kvp.Value;
                    await _unitOfWork.CompleteAsync();
                }
                throw new Exception(ex.Message);
            }
            return order;
        }
    }
}
