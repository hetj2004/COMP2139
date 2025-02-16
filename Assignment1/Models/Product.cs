
using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        [Required, StringLength(50)]
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int LowStockThreshold { get; set; }
        public int CategoryId { get; set; }
        [Required]
        public Category? Category { get; set; }
    }
}