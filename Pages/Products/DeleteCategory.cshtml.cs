using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.Models;
using System.Threading.Tasks;

namespace ProjectPRN221.Pages.Categories
{
    public class DeleteCategoryModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        [BindProperty]
        public Category Category { get; set; }

        public DeleteCategoryModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int categoryId)
        {
            Category = await _context.Categories.FindAsync(categoryId);

            if (Category == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int categoryId)
        {
            Category = await _context.Categories.FindAsync(categoryId);

            if (Category != null)
            {
                _context.Categories.Remove(Category);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Category deleted successfully!";
            }

            return RedirectToPage("/Products/Category");
        }
    }
}
