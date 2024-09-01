using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Models;
using MarketAPI.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;
        private readonly ApiContext _context;

        public OrdersController(IOrdersService ordersService, ApiContext context)
        {
            _ordersService = ordersService;
            _context = context;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Add(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is invalid");
            }

            Order order = new Order()
            {
                Address = model.Address,
                Buyer = await _context.Users.FirstAsync(x => x.Id == model.BuyerId),
                BuyerId = model.BuyerId,
                Seller = await _context.Sellers.FirstAsync(x => x.Id == model.SellerId),
                SellerId = model.SellerId,
                Price = model.Price,
                OfferId = model.OfferId,
                Offer = await _context.Offers.FirstAsync(x => x.Id == model.OfferId),
                Quantity = model.Quantity,
            };

            await _ordersService.AddOrderAsync(order);
            return Ok("Order added succesfully");
        }
    }
}
