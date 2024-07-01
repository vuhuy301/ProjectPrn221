﻿using System;
using System.Collections.Generic;

namespace ProjectPRN221.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
