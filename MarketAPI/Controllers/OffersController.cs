using MarketAPI.Data.Models;
using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Services.Offers;
using MarketAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly IOffersService _service;

        public OffersController(IOffersService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAll()
        {
            List<Offer> products = await _service.GetAllAsync();


            return Ok(products);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(OfferViewModel offer)
        {
            List<Offer> offers = await _service.GetAllAsync();
            if (!offers.Any(x => x.Title == offer.Title && x.OwnerId == offer.OwnerId))
            {
                await _service.AddOffer(new Offer()
                {
                    Title = offer.Title,
                    PricePerKG = offer.PricePerKG,
                    OfferTypeId = offer.OfferTypeId,
                    OwnerId = offer.OwnerId,
                    inSeason = offer.inSeason
                });
                return Ok("Offer Added Succesfuly");
            }
            else
            {
                return BadRequest("Offer already in Database");
            }
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditOffer(OfferViewModel offer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(offer);
            }

            await _service.EditAsync(offer);

            return Ok("Edited Succesfully");
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offers = await _service.GetAllAsync();

            if (!offers.Any(x => x.Id == id)) return BadRequest("Invalid Id");
            var offer = await _service.GetByIdAsync(id);

            await _service.RemoveByIdAsync(id);

            return Ok("Deleted Succesfully");
        }
    }
}
