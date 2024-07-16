
using Microsoft.EntityFrameworkCore;

namespace RouteSummitTask.BLL.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        private readonly OrderManagementDbContext _context;

        public CustomerRepository(OrderManagementDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Customer?> GetByUserId(string? userId)
            => await _context.Customers.FirstOrDefaultAsync(c => c.AppUserId == userId);
    }
}
