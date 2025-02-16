namespace Assignment1.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal TotalPrice { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}