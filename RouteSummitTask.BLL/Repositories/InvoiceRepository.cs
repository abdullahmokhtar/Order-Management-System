namespace RouteSummitTask.BLL.Repositories
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(OrderManagementDbContext context) : base(context)
        {
        }
    }
}
