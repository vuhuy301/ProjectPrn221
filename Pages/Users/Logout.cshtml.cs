using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages.Users
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnPost()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index"); 
        }
    }
}
