using MarketAPI.Data.Models;
using MarketAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MarketAPI.Services.Offers;
using MarketAPI.Models;

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
                    OwnerId = offer.OwnerId
                });
                return Ok("Offer Added Succesfuly");
            }
            else
            {
                return BadRequest("Offer already in Database");
            }
        }


        //TODO: Add method for editing
        //TODO: Add method for deleting
    }
}
