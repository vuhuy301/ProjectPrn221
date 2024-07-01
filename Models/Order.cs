using System;
using System.Collections.Generic;

namespace ProjectPRN221.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string? OrderStatus { get; set; }
        public string? ShippingAddress { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Notes { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
