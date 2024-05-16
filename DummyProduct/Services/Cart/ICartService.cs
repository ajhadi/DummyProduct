using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;

namespace DummyProduct.Services.Cart
{
    public interface ICartService
    {
        Task<ServiceStatus<List<CartDto>>> GetCartAsync();
        Task<ServiceStatus> AddProductToCartAsync(int productId);
        Task<ServiceStatus> DeleteProductAsync(int productId);
    }
}
