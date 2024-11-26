
using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Data.Models
{
    public class Seller : User
    {
        [Required]
        public double Rating { get; set; } = 0.0;
        public virtual ICollection<Order> SoldOrders { get; set; } = new List<Order>();
        public List<Offer> Offers { get; set; } = new List<Offer>();


    }
}
