using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;

namespace DummyProduct.Services.Product
{
    public interface IProductService
    {
        Task<ServiceStatus> CreateProductAsync(ProductDto product);
        Task<ServiceStatus<ProductDto>> GetProductAsync(int id);
    }
}
