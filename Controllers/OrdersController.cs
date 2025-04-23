using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Dtos.Order;
using OrderManagementApi.Entities;
using OrderManagementApi.Services.OrdersService;
using OrderManagementApi.Services.ProductsService;
using OrderManagementApi.SwaggerExamples;
using OrderManagementApi.Validators;
using Swashbuckle.AspNetCore.Filters;

namespace OrderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService,
            IProductService productService)
        {
            _productService = productService;
            _orderService = orderService;
        }

        [HttpPost]
        [SwaggerResponseExample(400, typeof(CreateOrderRequestDto))]
        [SwaggerResponseExample(201, typeof(CreateOrderRequestDto))]
        [SwaggerResponseExample(404, typeof(CreateOrderRequestDto))]
        [SwaggerRequestExample(typeof(CreateOrderRequestDto), typeof(OrderExample))]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequestDto orderDto)
        {
            // Check if the orderDto is null
            if (orderDto is null)
            {
                return BadRequest("Order data is null");
            }

            // Check if the orderDto is valid
            var validator = new OrderItemValidator();
            foreach (var item in orderDto.OrderItems)
            {
                var result = await validator.ValidateAsync(item);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(e => e.ErrorMessage));
                }
            }

            // Check if the given products exist and if there is enough stock
            /* caching --> */ var dic = new Dictionary<int, Product>();
            foreach (var item in orderDto.OrderItems)
            {
                if (dic.TryGetValue(item.ProductId, out var product))
                {
                    // Check stock
                    if (product.Stock < item.Quantity)
                    {
                        return BadRequest($"Not enough stock for product with id - {item.ProductId}");
                    }

                    // Update the stock
                    product.Stock -= item.Quantity;

                    _productService.Update(product);

                    continue;
                }
                else
                {
                    // Product does not exist in the dictionary, fetch it from the database
                    product = await _productService.GetByIdAsync(item.ProductId);
                }

                // Check if the product exists
                if (product is null)
                {
                    return NotFound($"There is no product with id - {item.ProductId}");
                }

                // Check if the product is in stock
                if (product.Stock < item.Quantity)
                {
                    return BadRequest($"Not enough stock for product with id - {item.ProductId}");
                }

                // Update the stock and add to the dictionary
                product.Stock -= item.Quantity;

                dic.Add(item.ProductId, product);

                _productService.Update(product);
            }

            // Map the Dto to Order
            var order = new Order
            {
                OrderItems = orderDto.OrderItems.Select(item => new OrderItem
                {
                    UnitPrice = item.UnitPrice,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                }).ToList(),
                TotalAmount = orderDto.OrderItems.Sum(item => item.UnitPrice * item.Quantity)
            };

            // Add the order to the database and save changes
            await _orderService.AddAsync(order);
            await _orderService.SaveChangesAsync();
            await _productService.SaveChangesAsync();

            // return the created order
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpGet("{id:int}")]
        [SwaggerResponseExample(200, typeof(Order))]
        [SwaggerResponseExample(404, typeof(Order))]
        public async Task<IActionResult> GetOrder(int id)
        {
            // Try to get the order by id
            var order = await _orderService.GetByIdAsync(id);

            // Check if the order is null
            if (order is null)
            {
                return NotFound($"There is no order with id - {id}");
            }

            // return the order
            return Ok(order);
        }
    }
}
