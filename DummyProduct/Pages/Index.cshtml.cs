using DummyProduct.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DummyProduct.Pages
{
    public class IndexModel : PageModel
    {
        public ProductViewModel ProductViewModel { get; private set; }
        private readonly ILogger<IndexModel> _logger;
        [BindProperty(SupportsGet = true)]
        public string SearchQuery { get; set; }

        public IndexModel(ILogger<IndexModel> logger,
            ProductViewModel productViewModel)
        {
            _logger = logger;
            ProductViewModel = productViewModel;
        }

        public async Task OnGetAsync(string searchQuery)
        {
            if (!string.IsNullOrEmpty(searchQuery))
            {
                await ProductViewModel.LoadProductsAsync(searchQuery);
            }
            else
            {
                await ProductViewModel.LoadProductsAsync();
            }
        }
    }
}
