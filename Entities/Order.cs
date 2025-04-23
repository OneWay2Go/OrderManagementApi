namespace OrderManagementApi.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        public decimal TotalAmount { get; set; }
    }
}
