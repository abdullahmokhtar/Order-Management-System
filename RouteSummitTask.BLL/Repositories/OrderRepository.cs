
using Microsoft.EntityFrameworkCore;

namespace RouteSummitTask.BLL.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly OrderManagementDbContext _context;

        public OrderRepository(OrderManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Order>> GetAllCustomerOrder(int? customerId)
            => await _context.Orders
            .Where(e => e.CustomerId == customerId)
            .Include(e => e.OrderItems).ToListAsync();


        public async Task<Order?> GetCustomerOrderDetailsAsync(int orderId, int? customerId)
            => await _context.Orders.
            FirstOrDefaultAsync(e => e.Id == orderId && e.CustomerId == customerId);
    }
}
