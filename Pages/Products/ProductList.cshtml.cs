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
		public int selectedId { get; set; }
		public string PriceOrder { get; set; }
		public IList<Product> Products { get; private set; }
		public IList<Category> categories { get; private set; }
		public async Task OnGetAsync(int category, string priceOrder)
		{
			
			categories = await _context.Categories.ToListAsync();
			selectedId = category;
			PriceOrder = priceOrder;
			var query = _context.Products.AsQueryable();
			if (category > 0)
			{
				query = query.Include(p => p.ProductImages).Where(p => p.CategoryId == category);
			}
			else
			{
				query = query.Include(p => p.ProductImages);
			}

			if (priceOrder == "asc")
			{
				Products = await query.OrderBy(p => p.Price).ToListAsync();
			}
			else if (priceOrder == "desc")
			{
				Products = await query.OrderByDescending(p => p.Price).ToListAsync();
			}
			else
			{
				Products = await query.ToListAsync();
			}


		}
	}
}
