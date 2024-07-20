using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Users
{
    public class LoginModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        public LoginModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnPostAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                HttpContext.Session.SetString("Username", user.FullName);
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetInt32("Role", user.RoleId);
                if(user.RoleId == 1)
                {
					return RedirectToPage("/Index");
				}
                else
                {
					return RedirectToPage("/Index");
				}
               
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid username or password.";
                return Page();
            }
        }
    }
}
