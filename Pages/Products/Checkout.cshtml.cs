using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectPRN221.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectPRN221.Pages.Products
{
	public class CheckoutModel : PageModel
	{
		private readonly ProjectPRNContext _context;

		public CheckoutModel(ProjectPRNContext context)
		{
			_context = context;
		}

		[BindProperty]
		public string Name { get; set; }
		[BindProperty]
		public string Phone { get; set; }
		[BindProperty]
		public string Address { get; set; }
		[BindProperty]
		public string OrderNotes { get; set; }
		[BindProperty]
		public decimal TotalPrice { get; set; } = 0;

		[BindProperty]
		public List<OrderItem> orderItems { get; set; } = new List<OrderItem>();

		public async Task<IActionResult> OnGetAsync(string query)
		{
			if (!string.IsNullOrEmpty(query))
			{
				var queryParams = query.Split('&');
				var productIds = new List<string>();
				var quantities = new List<int>();

				foreach (var param in queryParams)
				{
					var keyValue = param.Split('=');
					if (keyValue[0] == "productIds")
					{
						productIds.Add(keyValue[1]);
					}
					else if (keyValue[0] == "quantities")
					{
						if (int.TryParse(keyValue[1], out int quantity))
						{
							quantities.Add(quantity);
						}
					}
				}

				for (int i = 0; i < productIds.Count; i++)
				{
					var id = productIds[i];
					var quantity = quantities[i];
					Product product = await _context.Products.FirstOrDefaultAsync(m => m.ProductId == Int32.Parse(id));
					orderItems.Add(new OrderItem
					{
						OrderItemId = i,
						ProductId = Int32.Parse(id),
						Quantity = quantity,
						Product = product,
						UnitPrice = product.Price * quantity,
					});
					TotalPrice += product.Price * quantity;
				}
			}

			return Page();
		}

		public IActionResult OnPost()
		{
			var order = new Order
			{
				Name = Name,
				Phone = Phone,
				ShippingAddress = Address,
				PaymentMethod = "COD",
				Notes = OrderNotes,
				CreatedAt = DateTime.Now,
				TotalAmount = TotalPrice
			};
			if (HttpContext.Session.GetInt32("UserId") != null)
			{
				order.UserId = HttpContext.Session.GetInt32("UserId");
			}

			_context.Orders.Add(order);
			_context.SaveChanges();

			foreach (var orderItem in orderItems)
			{
				orderItem.OrderId = order.OrderId;
				_context.OrderItems.Add(orderItem);
			}

			_context.SaveChanges();
			return RedirectToPage("/Products/Thankyou");
		}
	}
}
