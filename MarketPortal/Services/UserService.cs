
using MarketPortal.Data.Models;
using System.Text.Json;

namespace MarketPortal.Services
{
    public class UserService : IUserService
    {
        private static readonly HttpClient client;
        static UserService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://farmers-market.somee.com/api")
            };
        }

        public async Task<User> Login(string email, string password)
        {
            var url = $"/users/login?email={email}&password={password}";
            var result = new User();
            var response = await client.GetAsync(url);
            Console.WriteLine(response);
            var stringResponse = await response.Content.ReadAsStringAsync();

            result = JsonSerializer.Deserialize<User>(stringResponse);

            return result;
        }
    }
}
