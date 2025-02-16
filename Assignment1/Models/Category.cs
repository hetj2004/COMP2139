using System.ComponentModel.DataAnnotations;

namespace Assignment1.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        
        [Required, StringLength(50)]
        public string? Name { get; set; }
        
        [Required, StringLength(50)]
        public string? Description { get; set; }
        
        public List<Product> Product { get; set; } = new List<Product>();
    }
}