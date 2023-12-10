using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Data.Models
{
    public class User
    {

        [Key]
        public Guid Id { get; set; }

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
        
        public List<Offer> Offers { get; set; } = new List<Offer>();

    }
}
