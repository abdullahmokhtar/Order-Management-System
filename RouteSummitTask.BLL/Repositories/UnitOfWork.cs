namespace RouteSummitTask.BLL.Repositories
{
    public class UnitOfWork(OrderManagementDbContext context) : IUnitOfWork
    {
        public IProductRepository ProductRepository { get; private set; } = new ProductRepository(context);
        public ICustomerRepository CustomerRepository { get; private set; } = new CustomerRepository(context);
        public IOrderRepository OrderRepository { get; private set; } = new OrderRepository(context);
        public IInvoiceRepository InvoiceRepository { get; private set; } = new InvoiceRepository(context);

        public async Task<int> CompleteAsync()
            => await context.SaveChangesAsync();


    }
}
