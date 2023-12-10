using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Services.Offers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace MarketAPI.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly ApiContext apiContext;

        public OffersService(ApiContext apiContext)
        {
            this.apiContext = apiContext;
        }

        public async Task AddOffer(Offer offer)
        {
            await apiContext.AddAsync(offer);
            await apiContext.SaveChangesAsync();

        }



        public async Task<List<Offer>> GetAllAsync()
        {
            return await apiContext.Offers.Include(x => x.Owner).ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await apiContext.Offers.Include(x => x.Owner).SingleAsync(x => x.Id == id);  
        }

    }
}
