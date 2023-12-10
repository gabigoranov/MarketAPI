using MarketAPI.Data.Models;

namespace MarketAPI.Services.Offers
{
    public interface IOffersService
    {
        Task<List<Offer>> GetAllAsync();
        Task AddOffer(Offer offer);
        Task<Offer> GetByIdAsync(int id);
    }
}
