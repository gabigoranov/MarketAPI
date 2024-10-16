﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(28)]
        public string Title { get; set; }

        [Required]
        [StringLength(16)]
        public string Town { get; set; }

        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        [Required]
        public double PricePerKG { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public Guid OwnerId { get; set; }
        public Seller Owner { get; set; }

        [Required]
        [ForeignKey(nameof(Stock))]
        public int StockId { get; set; }
        
        public Stock Stock { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();


    }
}
