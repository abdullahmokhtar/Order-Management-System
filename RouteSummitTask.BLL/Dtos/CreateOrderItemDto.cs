using System.ComponentModel.DataAnnotations;

namespace RouteSummitTask.BLL.Dtos
{
    public class CreateOrderItemDto
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }
        [Range(1, 50)]
        public int Quantity { get; set; }
    }
}
