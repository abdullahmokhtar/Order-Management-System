namespace RouteSummitTask.PL.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Order> Orders { get; set; } = [];
        public string AppUserId { get; set; } = string.Empty;
    }
}
