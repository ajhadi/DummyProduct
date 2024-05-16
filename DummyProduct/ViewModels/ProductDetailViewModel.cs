using DummyProduct.Models.DataTransferObjects;
using DummyProduct.Services.DummyApi;
using DummyProduct.Services.Product;
using System.Net;
using System.Threading.Tasks;

namespace DummyProduct.ViewModels
{
    public class ProductDetailViewModel
    {
        private readonly IDummyApiService dummyApiService;
        private readonly IProductService productService;

        public ProductDetailViewModel(IDummyApiService dummyApiService
            , IProductService productService)
        {
            this.dummyApiService = dummyApiService;
            this.productService = productService;
        }

        public ProductDto Product { get; private set; }

        public async Task LoadProductAsync(int productId)
        {
            var product = await productService.GetProductAsync(productId);
            if (product.Error?.HttpStatusCode == (int)HttpStatusCode.NotFound)
            {
                var getProduct = await dummyApiService.GetProductAsync(productId);
                if (getProduct.Result == null) return;
                await productService.CreateProductAsync(getProduct.Result);
                product = await productService.GetProductAsync(productId);
            }
            Product = product.Result;
        }
    }
}
