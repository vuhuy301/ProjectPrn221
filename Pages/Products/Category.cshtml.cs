using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectPRN221.Pages.Categories
{
    public class CategoryModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        public CategoryModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public IList<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _context.Categories.ToListAsync();
            return Page();
        }
    }
}
