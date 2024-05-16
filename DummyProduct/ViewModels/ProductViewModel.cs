using DummyProduct.Models.DataTransferObjects;
using DummyProduct.Services.DummyApi;
namespace DummyProduct.ViewModels
{
    public class ProductViewModel
    {
        private readonly IDummyApiService _dummyApiService;

        public ProductViewModel(IDummyApiService dummyApiService)
        {
            _dummyApiService = dummyApiService;
        }

        public List<ProductDto> Products { get; private set; }

        public async Task LoadProductsAsync()
        {
            var products = await _dummyApiService.GetProductsAsync();
            Products = products.Result;
        }
        public async Task LoadProductsAsync(string searchQuery)
        {
            var products = await _dummyApiService.GetProductsAsync(searchQuery);
            Products = products.Result;
        }
    }
}
