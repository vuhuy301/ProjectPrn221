using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Users
{
    public class MyOrderModel : PageModel
    {
        private readonly ProjectPRNContext _context;
        public List<Order> Orders { get; set; }
        public MyOrderModel(ProjectPRNContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            Orders = await _context.Orders.Where(x => x.UserId == HttpContext.Session.GetInt32("UserId")).Include(m => m.OrderItems).ToListAsync();
        }
    }
}
