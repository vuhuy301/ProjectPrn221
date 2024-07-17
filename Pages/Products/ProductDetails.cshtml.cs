using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Products
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        [BindProperty]
        public int ProductId { get; set; }
        public ProductDetailsModel(ProjectPRNContext context)
        {
            _context = context;
        }
        public Product Product { get; set; }
        public List<Product> FeaturedProducts { get; set; }
        public async Task<IActionResult> OnGetAsync(int productId)
        {
            Product = await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.ProductId == productId);

            if (Product == null)
            {
                return NotFound();
            }
            FeaturedProducts = await _context.Products
            .Where(p => p.ProductId != productId) 
            .Include(p => p.ProductImages) 
            .OrderBy(p => Guid.NewGuid())
            .Take(4)
            .ToListAsync();

            return Page();
        }
        public IActionResult OnPostProductDetails()
        {

            var cart = Request.Cookies["cart"];
            var cartItems = string.IsNullOrEmpty(cart) ? new List<int>() : new List<int>(Array.ConvertAll(cart.Split(','), int.Parse));


            cartItems.Add(ProductId);

            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("cart", string.Join(",", cartItems), options);

            return RedirectToPage("Cart");
        }
    }
}
