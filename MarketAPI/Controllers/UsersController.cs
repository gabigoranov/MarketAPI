using MarketAPI.Data;
using MarketAPI.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace MarketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiContext _context;

        public UsersController(ApiContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("login")]
        public IActionResult Login(string email, string password)
        {
            User user = _context.Users.Include(x => x.Offers).First(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                Console.WriteLine(user.Offers.Count);
                Console.WriteLine(user);
                return Ok(user);
            }

            return BadRequest("User with doesn't exist");
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddUser(User user)
        {
            if (!_context.Users.Any(u => u.Email == user.Email))
            {
                _context.Users.Add(new User() //
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email, 
                    PhoneNumber = user.PhoneNumber,
                    Age = user.Age,
                    isVerified = user.isVerified,
                    Description = user.Description,
                    Password = user.Password,
                });
                _context.SaveChanges();
                return Ok($"User: {user.FirstName} added to Database");
            }
            else
            {
                return BadRequest("User already in Database");
            }
        }
        
        //TODO: Implement service
        //TODO: Add method for editing
        //TODO: Add method for deleting
    
    }
}
