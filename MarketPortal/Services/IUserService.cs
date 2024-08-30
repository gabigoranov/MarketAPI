using MarketPortal.Data.Models;

namespace MarketPortal.Services
{
    public interface IUserService
    {
        public Task<User> Login(string email, string password);
    }
}
