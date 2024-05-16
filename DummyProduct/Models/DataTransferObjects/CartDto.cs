using DummyProduct.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DummyProduct.Models.DataTransferObjects
{
    public class CartDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
    }
}
