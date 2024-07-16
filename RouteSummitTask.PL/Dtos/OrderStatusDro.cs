using System.ComponentModel.DataAnnotations;

namespace RouteSummitTask.PL.Dtos
{
    public class OrderStatusDro
    {
        [Range(0, 6)]
        public Status OrderStatus { get; set; }
    }
}
