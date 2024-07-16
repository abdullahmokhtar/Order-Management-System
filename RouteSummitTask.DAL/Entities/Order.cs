namespace RouteSummitTask.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public List<OrderItem> OrderItems { get; set; } = [];
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public Status Status { get; set; } = Status.Pending;
        public Customer Customer { get; set; } = null!;
    }
}
