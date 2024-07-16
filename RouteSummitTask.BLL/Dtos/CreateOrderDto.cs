namespace RouteSummitTask.BLL.Dtos
{
    public class CreateOrderDto
    {
        public PaymentMethod PaymentMethod { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; } = [];
    }
}
