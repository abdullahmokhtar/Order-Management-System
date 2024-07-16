using System.ComponentModel.DataAnnotations;

namespace RouteSummitTask.PL.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}
