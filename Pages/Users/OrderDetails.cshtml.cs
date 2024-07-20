using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Users
{
    public class OrderDetailsModel : PageModel
    {
        private readonly ProjectPRNContext _context;

        public OrderDetailsModel(ProjectPRNContext context)
        {
            _context = context;
        }
        public List<OrderItem> orderItems { get; set; }

        public async Task<IActionResult> OnGetAsync(int orderId)
        {
            orderItems = await _context.OrderItems.Where(m => m.OrderId == orderId).Include(x => x.Product).ThenInclude(n => n.ProductImages).ToListAsync();
            return Page();
        }
    }
}
