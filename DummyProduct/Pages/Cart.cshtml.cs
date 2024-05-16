using DummyProduct.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DummyProduct.Pages
{
    public class CartModel : PageModel
    {
        public CartViewModel CartViewModel { get; private set; }
        private readonly ILogger<CartModel> _logger;

        public CartModel(ILogger<CartModel> logger,
            CartViewModel cartViewModel)
        {
            _logger = logger;
            CartViewModel = cartViewModel;
        }
        public async Task OnGetAsync()
        {
            await CartViewModel.LoadCartAsync();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var result = await CartViewModel.DeleteProductAsync(id);

            return RedirectToPage("Cart");
        }
    }
}
