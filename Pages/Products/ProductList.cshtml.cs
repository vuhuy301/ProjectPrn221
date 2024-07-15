using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;
using System;


namespace ProjectPRN221.Pages.Products
{
    public class ProductListModel : PageModel
    {
		private readonly ProjectPRNContext _context;

        public ProductListModel(ProjectPRNContext context)
		{
			_context = context;
		}
		public IList<Product> Products { get; private set; }
		public async Task OnGetAsync()
		{
			Products = await _context.Products.Include(p => p.ProductImages).ToListAsync();
		}
	}
}
