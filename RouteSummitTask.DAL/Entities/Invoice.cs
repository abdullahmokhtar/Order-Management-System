namespace RouteSummitTask.DAL.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Order Order { get; set; } = null!;
    }
}
