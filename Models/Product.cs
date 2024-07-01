using System;
using System.Collections.Generic;

namespace ProjectPRN221.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            ProductImages = new HashSet<ProductImage>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? CategoryId { get; set; }
        public bool? Status { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
