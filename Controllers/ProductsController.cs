using Microsoft.AspNetCore.Mvc;
using OrderManagementApi.Dtos.Product;
using OrderManagementApi.Entities;
using OrderManagementApi.Services.ProductsService;
using OrderManagementApi.SwaggerExamples;
using OrderManagementApi.Validators;
using Swashbuckle.AspNetCore.Filters;

namespace OrderManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id:int}")]
        [SwaggerResponseExample(200, typeof(Product))]
        [SwaggerResponseExample(404, typeof(Product))]
        public async Task<IActionResult> GetProduct(int id)
        {
            // Check if the given id is valid
            var product = await _productService.GetByIdAsync(id);

            // Check if the product exists
            if (product is null)
            {
                return NotFound($"There is no product with id - {id}");
            }

            // Return the product
            return Ok(product);
        }

        [HttpPost]
        [SwaggerResponseExample(201, typeof(Product))]
        [SwaggerResponseExample(400, typeof(Product))]
        [SwaggerRequestExample(typeof(CreateProductRequestDto), typeof(ProductExample))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDto productDto)
        {
            // Check if the given dto is valid
            var validator = new ProductValidator();
            var result = await validator.ValidateAsync(productDto);
            if (!result.IsValid)
            {
                return BadRequest(result.Errors.Select(e => e.ErrorMessage));
            }

            // Map the Dto to Product
            var product = new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            };

            // Add to the database and save changes
            await _productService.AddAsync(product);
            await _productService.SaveChangesAsync();

            // Return the created product
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id:int}/stock")]
        [SwaggerResponseExample(200, typeof(Product))]
        [SwaggerResponseExample(400, typeof(Product))]
        [SwaggerResponseExample(404, typeof(Product))]
        public async Task<IActionResult> UpdateProductStock(int id, [FromBody] UpdateStockRequestDto productDto)
        {
            // Check if the given stock is valid
            if (productDto.Stock <= 0)
            {
                return BadRequest("Stock have to be more than one");
            }

            // Check if the given id is valid
            var product = await _productService.GetByIdAsync(id);

            // Check if the product exists
            if (product is null)
            {
                return NotFound($"There is no product with id - {id}");
            }

            // Update the product stock
            product.Stock = productDto.Stock;

            // Update the product in the database and save changes
            _productService.Update(product);
            await _productService.SaveChangesAsync();

            // Return Ok response
            return Ok("Stock updated successfully");
        }
    }
}
