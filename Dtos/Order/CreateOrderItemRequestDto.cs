namespace OrderManagementApi.Dtos.Order
{
    public class CreateOrderItemRequestDto
    {
        public int UnitPrice { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
