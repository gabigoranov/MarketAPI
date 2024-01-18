using MarketAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models
{
    public class UserViewModel
    {
        [Required]
        [StringLength(16)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public bool isVerified { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(24, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(220)]
        public string Description { get; set; }

        [Required]
        public IFormFile PictureFile { get; set; }

        public List<Offer> Offers { get; set; } = new List<Offer>();
    }
}
