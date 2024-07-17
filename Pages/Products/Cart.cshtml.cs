using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Products
{
    public class CartModel : PageModel
    {
        

        private readonly ProjectPRNContext _context;
        public List<int> ProductIds { get; private set; } = new List<int>();
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



        private async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Include(m => m.ProductImages).FirstOrDefaultAsync(p => p.ProductId == id);
        }
    } 
}
