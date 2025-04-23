using OrderManagementApi.Entities;

namespace OrderManagementApi.Services.ProductsService
{
    public class ProductService : IProductService
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
    }
}
