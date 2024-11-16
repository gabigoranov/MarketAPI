using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Models;
using MarketAPI.Services.Firebase;
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
        private readonly FirebaseService _firebaseService;
        private readonly ApiContext _context;

        public OrdersController(IOrdersService ordersService, ApiContext context, FirebaseService firebaseService)
        {
            _ordersService = ordersService;
            _context = context;
            _firebaseService = firebaseService;
        }

        [HttpGet]
        [Route("getall")]
        //for testing only
        public async Task<IActionResult> GetAll()
        {
            List<Order> orders = await _context.Orders.Include(x => x.Offer).ToListAsync();
            return Ok(orders);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("ModelState is invalid");
            }

            Order order = new Order()
            {
                Address = model.Address,
                IsApproved = false,
                DateOrdered = DateTime.UtcNow,
                SellerId = model.SellerId,
                Title = model.Title,
                BuyerId = model.BuyerId,
                Price = model.Price,
                OfferId = model.OfferId,
                OfferTypeId = model.OfferTypeId,
            };

            await _context.Orders.AddAsync(order);
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
            return Ok("Approved purchase succesfully");
        }

        [HttpGet]
        [Route("decline")]
        public async Task<IActionResult> Decline(int id)
        {
            Order order = _context.Orders.Include(x => x.Offer).First(x => x.Id == id);
            _context.Orders.Remove(order);
            _context.Stocks.Single(x => x.Id == order.Offer.StockId).Quantity -= order.Quantity;
            await _context.SaveChangesAsync();
            return Ok("Declined purchase successfully");
        }

        [HttpGet]
        [Route("deliver")]
        public async Task<IActionResult> Deliver(int id)
        {
            Order purchase = _context.Orders.Include(x => x.Buyer).Single(x => x.Id == id);
            _context.Update(purchase);
            purchase.IsDelivered = true;
            purchase.DateDelivered = DateTime.Now;
            await _context.SaveChangesAsync();

            string? userToken = purchase.Buyer.FirebaseToken;

            if (!string.IsNullOrEmpty(userToken))
            {
                await _firebaseService.SendNotification(userToken, "Order Delivered", "Your order has been successfully delivered!", id);
            }

            return Ok("Purchase delivered succesfully");
        }

        
    }
}