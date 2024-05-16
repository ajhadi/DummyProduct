using Azure.Core;
using DummyProduct.Data;
using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;
using DummyProduct.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace DummyProduct.Services.Cart
{
    public class CartService : ICartService
    {
        private readonly DataContext context;
        private readonly ILogger<CartService> logger;
        public CartService(DataContext context,
        ILogger<CartService> logger)
        {
            this.context = context;
            this.logger = logger;
        }

        public async Task<ServiceStatus<List<CartDto>>> GetCartAsync()
        {
            try
            {
                var cart = await context.Cart
                        .Include(c => c.Product)
                        .OrderByDescending(c => c.Id)
                        .Select(c => new CartDto
                        {
                            Id = c.Id,
                            Product = new ProductDto
                            {
                                Id = c.Product.Id,
                                Title = c.Product.Title,
                                Description = c.Product.Description,
                                Price = c.Product.Price,
                                DiscountPercentage = c.Product.DiscountPercentage,
                                Rating = c.Product.Rating,
                                Stock = c.Product.Stock,
                                Brand = c.Product.Brand,
                                Category = c.Product.Category,
                                Thumbnail = c.Product.Thumbnail
                            }
                        })
                        .ToListAsync();


                return ServiceStatus.SuccessObjectResult(cart);
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult<List<CartDto>>();
            }

        }
        public async Task<ServiceStatus> DeleteProductAsync(int id)
        {
            try
            {
                var cartItem = await context.Cart.FirstOrDefaultAsync(c => c.Id == id);
                if (cartItem == null)
                {
                    var error = new Error
                    {
                        HttpStatusCode = (int)HttpStatusCode.NotFound,
                        Code = 404,
                        Message = "Product not found in the cart"
                    };
                    return ServiceStatus.ErrorResult(error);
                }

                context.Cart.Remove(cartItem);
                await context.SaveChangesAsync();

                return ServiceStatus.SuccessResult();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }
        }

        public async Task<ServiceStatus> AddProductToCartAsync(int productId)
        {
            try
            {
                var newCartItem = new Models.Entities.Cart { ProductId = productId };
                context.Cart.Add(newCartItem);
                await context.SaveChangesAsync();

                return ServiceStatus.SuccessResult();
            }
            catch (Exception e)
            {
                logger.LogError(e.Message);
                return ServiceStatus.ErrorResult();
            }
        }

    }
}
