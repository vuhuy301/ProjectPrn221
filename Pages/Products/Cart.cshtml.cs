using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Products
{
    public class CartModel : PageModel
    {
        
        public List<OrderItem> orderItems { get; set; }

        private readonly ProjectPRNContext _context;
        public List<int> ProductIds { get; private set; } = new List<int>();

		[BindProperty]
		public List<string> PIds { get; set; }

		[BindProperty]
		public List<int> Quantities { get; set; }
		public List<Product> Products { get; private set; } = new List<Product>();
        public CartModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            var cart = Request.Cookies["cart"];
            if (!string.IsNullOrEmpty(cart))
            {
                var productIds = cart.Split(',')
                                     .Select(id => int.TryParse(id, out var productId) ? productId : (int?)null)
                                     .Where(id => id.HasValue)
                                     .Select(id => id.Value)
                                     .ToList();

                // Lấy từng sản phẩm từ cơ sở dữ liệu
                foreach (var id in productIds)
                {
                    var product = await GetProductById(id); 
                    if (product != null)
                    {
                        Products.Add(product);
                    }
                }
            }
        }
		public IActionResult OnPost()
		{
			var queryString = new List<string>();

			for (int i = 0; i < PIds.Count; i++)
			{
				var id = PIds[i];
				var quantity = Quantities[i];
				queryString.Add($"productIds={id}&quantities={quantity}");
			}

			var query = string.Join("&", queryString);
			return RedirectToPage("/Products/Checkout", new { query });
		}


		private async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(m => m.ProductImages).FirstOrDefaultAsync(p => p.ProductId == id);
        }

		
	}
	
}
