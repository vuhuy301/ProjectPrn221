using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.Models;
using Microsoft.EntityFrameworkCore;


namespace ProjectPRN221.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        public AddProductModel(ProjectPRNContext context)
        {
            _context = context;
        }
        public IList<Category> categories { get; private set; }
        public async Task OnGetAsync()
        {

            categories = await _context.Categories.ToListAsync();
          
        }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public string ImageUrl { get; set; }

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			Product.Description = Product.Description.Replace(" ", "\n ");

			_context.Products.Add(Product);
			await _context.SaveChangesAsync();

			if (!string.IsNullOrEmpty(ImageUrl))
			{
				var productImage = new ProductImage
				{
					ProductId = Product.ProductId,
					ImageUrl = ImageUrl
				};

				_context.ProductImages.Add(productImage);
				await _context.SaveChangesAsync();
			}

			TempData["SuccessMessage"] = "Product added successfully!";
			return RedirectToPage("/Products/ProductList");
		}

	}
}

