using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ProjectPRN221.Pages.Products
{
    public class DeleteCartModel : PageModel
    {
        public void OnGet()
        {
        }
        public IActionResult OnPost(int productId)
        {

            var cart = Request.Cookies["cart"];
            var cartItems = string.IsNullOrEmpty(cart) ? new List<int>() : new List<int>(Array.ConvertAll(cart.Split(','), int.Parse));


            cartItems.Remove(productId);


            var options = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(7)
            };
            Response.Cookies.Append("cart", string.Join(",", cartItems), options);

            return RedirectToPage("Cart");
        }
    }
}
