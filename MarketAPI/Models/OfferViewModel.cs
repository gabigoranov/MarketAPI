using MarketAPI.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Models
{
    public class OfferViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string Title { get; set; }

        [Required]
        public double PricePerKG { get; set; }

        [Required]
        public bool inSeason { get; set; }

        [Required]
        public int OfferTypeId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }
    }
}
