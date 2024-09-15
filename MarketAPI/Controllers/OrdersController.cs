using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Models;
using MarketAPI.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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

        [HttpGet]
        [Route("getall")]
        //for testing only
        public async Task<IActionResult> GetAllOrders()
        {
            List<Order> orders = await _context.Orders.ToListAsync();
            return Ok(orders);
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
                Title = model.Title,
                IsApproved = false,
                DateOrdered = DateTime.Now,
            };

            await _ordersService.AddOrderAsync(order);
            _context.Stocks.FirstAsync(x => x.Id == order.Offer.StockId).Result.Quantity -= order.Quantity;
            await _context.SaveChangesAsync();

            return Ok("Order added succesfully");
        }

        [HttpGet]
        [Route("accept")]
        //seller accepts order and stock is decreased
        public async Task<IActionResult> Accept(int id)
        {
            Order order = _context.Orders.Include(x => x.Offer).First(x => x.Id == id);
            _context.Update(order);
            order.IsApproved = true;
            _context.Stocks.Single(x => x.Id == order.Offer.StockId).Quantity -= order.Quantity;
            await _context.SaveChangesAsync();
            return Ok("Approved order succesfully");
        }

        [HttpGet]
        [Route("decline")]
        public async Task<IActionResult> Decline(int id)
        {
            _context.Orders.Remove(_context.Orders.First(x => x.Id == id));
            await _context.SaveChangesAsync();
            return Ok("Declined order");
        }
    }
}
