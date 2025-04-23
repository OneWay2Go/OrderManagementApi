using OrderManagementApi.Dtos.Order;
using Swashbuckle.AspNetCore.Filters;

namespace OrderManagementApi.SwaggerExamples
{
    public class OrderExample : IExamplesProvider<CreateOrderRequestDto>
    {
        public CreateOrderRequestDto GetExamples()
        {
            return new CreateOrderRequestDto
            {
                OrderItems = new List<CreateOrderItemRequestDto>
                {
                    new CreateOrderItemRequestDto
                    {
                        ProductId = 1,
                        Quantity = 2,
                        UnitPrice = 1
                    },
                    new CreateOrderItemRequestDto
                    {
                        ProductId = 2,
                        Quantity = 3,
                        UnitPrice = 2
                    }
                }
            };
        }
    }
}
