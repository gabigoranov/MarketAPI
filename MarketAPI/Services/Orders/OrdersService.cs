using MarketAPI.Data;
using MarketAPI.Data.Models;

namespace MarketAPI.Services.Orders
{
    public class OrdersService : IOrdersService
    {
        private readonly ApiContext _context;

        public OrdersService(ApiContext apiContext)
        {
            this._context = apiContext;
        }
        public Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
