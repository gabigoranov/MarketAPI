using MarketAPI.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models
{
    public class OrderViewModel
    {
        [Required]
        public int OfferId { get; set; }

        [Required]
        public Guid BuyerId { get; set; }

        [Required]
        public Guid SellerId { get; set; }

        [Required]
        public double Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
