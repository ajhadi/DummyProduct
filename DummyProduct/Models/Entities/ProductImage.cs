using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DummyProduct.Models.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string ImageUrl { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
