using System.ComponentModel.DataAnnotations;

namespace DummyProduct.Models.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public decimal DiscountPercentage { get; set; }

        [Range(0, 5)]
        public decimal Rating { get; set; }

        [Required]
        public int Stock { get; set; }

        [MaxLength(255)]
        public string Brand { get; set; }

        [MaxLength(255)]
        public string Category { get; set; }

        [MaxLength(255)]
        public string Thumbnail { get; set; }

        public ICollection<ProductImage> Images { get; set; }
    }
}
