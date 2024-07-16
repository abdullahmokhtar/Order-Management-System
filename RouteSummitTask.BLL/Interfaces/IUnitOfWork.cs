namespace RouteSummitTask.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IOrderRepository OrderRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }

        Task<int> CompleteAsync();
    }
}
