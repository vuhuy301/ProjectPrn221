using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Categories
{
    public class AddCategoryModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        [BindProperty]
        public Category Category { get; set; }

        public AddCategoryModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Categories.Add(Category);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Category added successfully!";
            return RedirectToPage("/Products/Category");
        }
    }
}
