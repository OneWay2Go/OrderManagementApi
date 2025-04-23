using OrderManagementApi.Dtos.Product;
using OrderManagementApi.Entities;

namespace OrderManagementApi.Services.ProductsService
{
    public interface IProductService
    {
        Task<Product> GetByIdAsync(int id);

        Task AddAsync(Product product);

        void Update(Product product);

        Task<int> SaveChangesAsync();
    }
}
