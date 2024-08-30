using System.ComponentModel.DataAnnotations;

namespace MarketAPI.Data.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; } 
    }
}
