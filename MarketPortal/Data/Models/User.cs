﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketPortal.Data.Models
{
    public class User : IdentityUser
    {

        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(12)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(12)]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public double Rating { get; set; } = 0.0;

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
        public string Town { get; set; }
        public List<Offer> Offers { get; set; } = new List<Offer>();

        [Required]
        public bool isSeller { get; set; }

        

    }
}
