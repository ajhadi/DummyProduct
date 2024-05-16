using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;

namespace DummyProduct.Services.DummyApi
{
    public interface IDummyApiService
    {
        Task<ServiceStatus<List<ProductDto>>> GetProductsAsync();
        Task<ServiceStatus<List<ProductDto>>> GetProductsAsync(string searchQuery);
        Task<ServiceStatus<ProductDto>> GetProductAsync(int id);
    }
}
