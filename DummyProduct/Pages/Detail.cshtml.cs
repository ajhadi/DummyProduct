using DummyProduct.Models.Entities;
using DummyProduct.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DummyProduct.Pages
{
    public class DetailModel : PageModel
    {
        public ProductDetailViewModel ProductViewModel { get; private set; }
        private CartViewModel CartViewModel { get; set; }
        private readonly ILogger<DetailModel> _logger;

        public DetailModel(ILogger<DetailModel> logger,
            ProductDetailViewModel productViewModel,
            CartViewModel cartViewModel)
        {
            _logger = logger;
            ProductViewModel = productViewModel;
            CartViewModel = cartViewModel;
        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            await ProductViewModel.LoadProductAsync(id);
            if (ProductViewModel.Product == null) return NotFound();
            return Page();
        }
        public async Task<IActionResult> OnPostBuyAsync(int id)
        {
            await CartViewModel.InsertProductAsync(id);
            return RedirectToPage("/Cart");
        }
    }
}
