using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectPRN221.Pages.Products
{
    public class UpdateProductModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        public UpdateProductModel(ProjectPRNContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId)
        {
            Product = await _context.Products
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (Product == null)
            {
                return NotFound();
            }

            Categories = await _context.Categories.ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }

            var productToUpdate = await _context.Products.FindAsync(Product.ProductId);

            if (productToUpdate == null)
            {
                return NotFound();
            }

            productToUpdate.ProductName = Product.ProductName;
            productToUpdate.Description = Product.Description;
            productToUpdate.Price = Product.Price;
            productToUpdate.Quantity = Product.Quantity;
            productToUpdate.CategoryId = Product.CategoryId;

            await _context.SaveChangesAsync();

            TempData["Message"] = "Cập nhật sản phẩm thành công.";
            return RedirectToPage("/Products/ProductDetails", new { productId = Product.ProductId });
        }
    }
}
