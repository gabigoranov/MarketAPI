using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Data.Models
{
    public class OfferType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
