using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Models;
using MarketAPI.Services.Offers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Net;

namespace MarketAPI.Services.Offers
{
    public class OffersService : IOffersService
    {
        private readonly ApiContext _context;

        public OffersService(ApiContext apiContext)
        {
            this._context = apiContext;
        }

        public async Task AddOffer(Offer offer)
        {
            await _context.AddAsync(offer);
            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(OfferViewModel offerEdit)
        {
            Offer offer =  await _context.Offers.Include(x => x.Owner).SingleAsync(x => x.Id == offerEdit.Id);
            _context.Update(offer);

            offer.Title = offerEdit.Title;
            offer.PricePerKG = offerEdit.PricePerKG;

            _context.Entry(offer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offers.Include(x => x.Owner).Take(500).ToListAsync();
        }

        public async Task<Offer> GetByIdAsync(int id)
        {
            return await _context.Offers.Include(x => x.Owner).SingleAsync(x => x.Id == id);  
        }

        public async Task RemoveByIdAsync(int id)
        {
            var offer = await _context.Offers.SingleAsync(x => x.Id == id);
            _context.Offers.Remove(offer);

            await _context.SaveChangesAsync();
        }
    }
}
