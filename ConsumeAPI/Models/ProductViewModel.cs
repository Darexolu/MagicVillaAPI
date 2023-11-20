using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumeAPI.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Villa Name")]
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        public int Occupancy { get; set; }
        public int Sqft { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
        public string? ThumbnailUrl { get; set; }
    }
}
