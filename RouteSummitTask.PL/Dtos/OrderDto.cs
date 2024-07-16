namespace RouteSummitTask.PL.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalAmount { get; set; }
        public List<OrderItemDto> OrderItems { get; set; } = [];
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;
        public Status Status { get; set; }
    }
}
