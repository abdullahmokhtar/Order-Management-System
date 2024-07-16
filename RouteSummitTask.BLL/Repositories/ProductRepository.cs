namespace RouteSummitTask.BLL.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(OrderManagementDbContext context) : base(context) { }
    }
}
