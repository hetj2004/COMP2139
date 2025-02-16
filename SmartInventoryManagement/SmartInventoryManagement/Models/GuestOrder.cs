using System.ComponentModel.DataAnnotations;

namespace SmartInventoryManagement.Models;

public class GuestOrder
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string GuestName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string GuestEmail { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public decimal TotalPrice { get; set; }

   public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

}
