using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MarketAPI.Data.Models
{
    public class Offer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(14)]
        public string Title { get; set; }

        [Required]
        public double PricePerKG { get; set; }
        [Required]
        [ForeignKey(nameof(User))]
        public Guid OwnerId { get; set; }
        public User Owner { get; set; }

        [Required]
        public bool inSeason { get; set; }

        [Required]
        [ForeignKey(nameof(OfferType))]
        public int OfferTypeId { get; set; }

        public OfferType OfferType { get; set; }
    }
}
