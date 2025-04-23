namespace OrderManagementApi.Dtos.Product
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
    }
}
