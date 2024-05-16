using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;
using DummyProduct.Models.Entities;
using DummyProduct.Services.Cart;
namespace DummyProduct.ViewModels
{
    public class CartViewModel
    {
        private readonly ICartService cartService;

        public CartViewModel(ICartService cartService)
        {
            this.cartService = cartService;
        }

        public List<CartDto> Cart { get; private set; }
        public decimal TotalPrice { get; private set; }

        public async Task LoadCartAsync()
        {
            var cart = await cartService.GetCartAsync();

            Cart = cart.Result;
            TotalPrice = Cart?.Sum(p => p.Product?.Price) ?? 0;
        }
        public async Task<ServiceStatus> InsertProductAsync(int productId)
        {
            return await cartService.AddProductToCartAsync(productId);
        }
        public async Task<ServiceStatus> DeleteProductAsync(int id)
        {
            return await cartService.DeleteProductAsync(id);
        }
    }
}
