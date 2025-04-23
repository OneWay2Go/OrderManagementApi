using OrderManagementApi.Dtos.Product;
using Swashbuckle.AspNetCore.Filters;

namespace OrderManagementApi.SwaggerExamples
{
    public class ProductExample : IExamplesProvider<CreateProductRequestDto>
    {
        public CreateProductRequestDto GetExamples()
        {
            return new CreateProductRequestDto
            {
                Name = "Sample Product",
                Price = 19.99m,
                Stock = 100
            };
        }
    }
}
