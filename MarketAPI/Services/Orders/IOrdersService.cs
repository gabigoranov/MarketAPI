using MarketAPI.Data.Models;

namespace MarketAPI.Services.Orders
{
    public interface IOrdersService
    {
        public Task AddOrderAsync(Order order);
    }
}
