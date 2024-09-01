
namespace MarketAPI.Data.Models
{
    public class Seller : User
    {
        public virtual ICollection<Order> SoldOrders { get; set; } = new List<Order>();

    }
}
