using MarketAPI.Data;
using MarketAPI.Data.Models;
using MarketAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            User? user = _context.Users.Include(x => x.Offers).FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("User with data doesn't exist");
        }

        [HttpGet]
        [Route("getWithId")]
        public IActionResult getWithId(Guid id)
        {
            User? user = _context.Users.Include(x => x.Offers).FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return Ok(user);
            }

            return BadRequest("User with id doesn't exist");
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
                    Description = user.Description,
                    Password = user.Password,
                    Rating = 0.0,
                    Town = user.Town,
                    isSeller = user.isSeller,
                });
                _context.SaveChanges();
                return Ok($"User: {user.FirstName} added to Database");
            }
            else
            {
                return BadRequest("User with email already in Database");
            }
        }

        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditUser(User userEdit)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(userEdit);
            }
            User user = await _context.Users.Include(x => x.Offers).SingleAsync(x => x.Id == userEdit.Id);
            _context.Update(user);

            user.Age = userEdit.Age;
            user.PhoneNumber = userEdit.PhoneNumber;
            user.Email = userEdit.Email;
            user.FirstName = userEdit.FirstName;    
            user.LastName = userEdit.LastName;
            user.Description = userEdit.Description;

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok("Edited Succesfully");    
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (!_context.Users.Any(x => x.Id == id)) return BadRequest("Invalid Id");    
            var user = await _context.Users.SingleAsync(x => x.Id == id);
            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return Ok("Deleted Succesfully");
        }

    }
}
