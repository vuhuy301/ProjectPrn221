using System;
using System.Collections.Generic;

namespace ProjectPRN221.Models
{
    public partial class Banner
    {
        public int BannerId { get; set; }
        public string? Title { get; set; }
        public string? ImageUrl { get; set; }
        public string? Link { get; set; }
        public int? DisplayOrder { get; set; }
        public bool? Status { get; set; }
    }
}
