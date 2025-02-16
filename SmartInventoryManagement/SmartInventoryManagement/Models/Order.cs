using System;
using System.Collections.Generic;

namespace SmartInventoryManagement.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderProduct> OrderProducts { get; set; }
    }

    
}