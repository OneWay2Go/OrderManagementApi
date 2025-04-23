using OrderManagementApi.Entities;

namespace OrderManagementApi.Services.OrdersService
{
    public interface IOrderService
    {
        Task AddAsync(Order order);

        Task<Order> GetByIdAsync(int id);

        Task<int> SaveChangesAsync();
    }
}
