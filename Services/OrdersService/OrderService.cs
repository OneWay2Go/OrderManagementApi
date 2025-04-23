using Microsoft.EntityFrameworkCore;
using OrderManagementApi.Entities;

namespace OrderManagementApi.Services.OrdersService
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;

        public OrderService(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
        }
        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
