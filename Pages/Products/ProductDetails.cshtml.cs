using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Products
{
    public class ProductDetailsModel : PageModel
    {
        private readonly ProjectPRNContext _context;
        public string Message { get; set; }
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

            if (cartItems.Contains(ProductId))
            {
                TempData["Message"] = "Sản phẩm đã có trong giỏ hàng.";
                return RedirectToPage("/Products/ProductDetails", new { productId = ProductId });
            }

            cartItems.Add(ProductId);

			var options = new CookieOptions
			{
				Expires = DateTime.Now.AddDays(7)
			};
			Response.Cookies.Append("cart", string.Join(",", cartItems), options);

			return RedirectToPage("Cart");
		}
        public async Task<IActionResult> OnPostDeleteAsync(int productId)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                return NotFound();
            }
            var orderItems = _context.OrderItems.Where(oi => oi.ProductId == productId);
            _context.OrderItems.RemoveRange(orderItems);
            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            _context.ProductImages.RemoveRange(images);

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công.";
            return RedirectToPage("/Products/ProductList");
        }

    }
}
