global using RouteSummitTask.DAL.Entities;

namespace RouteSummitTask.BLL.Interfaces
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<Customer?> GetByUserId(string? userId);
    }
}
