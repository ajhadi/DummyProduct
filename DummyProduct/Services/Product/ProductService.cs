using DummyProduct.Data;
using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;
using DummyProduct.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DummyProduct.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly DataContext context;
        private readonly ILogger<ProductService> logger;
        public ProductService(DataContext context,
        ILogger<ProductService> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<ServiceStatus> CreateProductAsync(ProductDto product)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    await context.Database.ExecuteSqlAsync($"SET IDENTITY_INSERT [dbo].[Products] ON");

                    var newProduct = new Models.Entities.Product
                    {
                        Id = product.Id,
                        Title = product.Title,
                        Description = product.Description,
                        Price = product.Price,
                        DiscountPercentage = product.DiscountPercentage,
                        Rating = product.Rating,
                        Stock = product.Stock,
                        Brand = product.Brand,
                        Category = product.Category,
                        Thumbnail = product.Thumbnail
                    };

                    context.Products.Add(newProduct);

                    if (product.Images != null && product.Images.Any())
                    {
                        foreach (var imageUrl in product.Images)
                        {
                            var productImage = new ProductImage
                            {
                                ImageUrl = imageUrl,
                                ProductId = product.Id
                            };

                            context.ProductImages.Add(productImage);
                        }

                    }
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlAsync($"SET IDENTITY_INSERT [dbo].[Products] OFF");
                    transaction.Commit();
                }
                return ServiceStatus.SuccessResult();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }
        }

        public async Task<ServiceStatus<ProductDto>> GetProductAsync(int id)
        {
            try
            {
                var product = await context.Products
                    .Include(p => p.Images)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (product == null)
                {
                    var error = new Error
                    {
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Code = 404,
                        Message = "Product not found"
                    };
                    return ServiceStatus.ErrorResult<ProductDto>(error);
                }

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    DiscountPercentage = product.DiscountPercentage,
                    Rating = product.Rating,
                    Stock = product.Stock,
                    Brand = product.Brand,
                    Category = product.Category,
                    Thumbnail = product.Thumbnail,
                    Images = product.Images?.Select(i => i.ImageUrl).ToList()
                };

                return ServiceStatus.SuccessObjectResult(productDto);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<ProductDto>();
            }
        }
    }
}
