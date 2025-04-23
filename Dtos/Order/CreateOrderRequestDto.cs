namespace OrderManagementApi.Dtos.Order
{
    public class CreateOrderRequestDto
    {
        public List<CreateOrderItemRequestDto> OrderItems { get; set; }
    }
}
