using MarketAPI.Data.Models;
using MarketAPI.Models;

namespace MarketAPI.Services.Offers
{
    public interface IOffersService
    {
        Task<List<Offer>> GetAllAsync();
        Task AddOffer(Offer offer);
        Task<Offer> GetByIdAsync(int id);
        Task RemoveByIdAsync(int id);

        Task EditAsync(OfferViewModel offerEdit);
    }
}
