using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;

namespace ProjectPRN221.Pages.Users
{
    public class ManageOrderModel : PageModel
    {
        private readonly ProjectPRN221.Models.ProjectPRNContext _context;
        public ManageOrderModel(ProjectPRNContext dbContext) 
        { 
            _context = dbContext;
        }
        public IList<ProjectPRN221.Models.Order> OrderList { get; set; }
        [BindProperty(SupportsGet =true)]
        public string? SearchString { get; set; }
        public SelectList? Name {  get; set; }

        public async Task OnGetAsync() 
        {
            if(_context.Orders != null)
            {
                OrderList = await _context.Orders.
                    Include(p => p.OrderItems).ToListAsync();
                var orders = from s in _context.Orders
                                 select s;
                if (!string.IsNullOrEmpty(SearchString))
                {
                    orders = orders.Where(s => s.Name.Contains(SearchString));
                }
                OrderList = await orders.ToListAsync();
            }
        }
    }
}
