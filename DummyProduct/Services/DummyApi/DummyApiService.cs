using DummyProduct.Models;
using DummyProduct.Models.DataTransferObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DummyProduct.Services.DummyApi
{
    public class DummyApiService : IDummyApiService
    {
        private readonly HttpClient client;
        private readonly ILogger<DummyApiService> logger;
        public DummyApiService(HttpClient client, ILogger<DummyApiService> logger)
        {
            this.client = client;
            this.logger = logger;
        }
        public async Task<ServiceStatus<List<ProductDto>>> GetProductsAsync()
        {
            try
            {
                var url = new Uri($"/products", UriKind.Relative);

                var request = new HttpRequestMessage()
                {
                    RequestUri = url,
                    Method = HttpMethod.Get
                };
                var send = await client.SendAsync(request);
                var content = await send
                    .Content.ReadAsStringAsync();

                var jsonObject = JObject.Parse(content);
                var productsArray = jsonObject["products"];
                var result = JsonConvert.DeserializeObject<List<ProductDto>>(productsArray.ToString());

                if (send.IsSuccessStatusCode)
                    return ServiceStatus.SuccessObjectResult<List<ProductDto>>(result);
                return ServiceStatus.ErrorResult<List<ProductDto>>(
                    Error.Create((int)send.StatusCode, 0, "Something went wrong")
                );
            }
            catch (Exception e)
            {
                logger.LogError($"error : {e.Message}, {e.StackTrace}");
                return ServiceStatus.ErrorResult<List<ProductDto>>();
            }
        }
        public async Task<ServiceStatus<List<ProductDto>>> GetProductsAsync(string searchQuery)
        {
            try
            {
                var url = new Uri($"/products/search?q={searchQuery}", UriKind.Relative);

                var request = new HttpRequestMessage()
                {
                    RequestUri = url,
                    Method = HttpMethod.Get
                };

                var send = await client.SendAsync(request);
                var content = await send.Content.ReadAsStringAsync();

                if (send.IsSuccessStatusCode)
                {
                    var jsonObject = JObject.Parse(content);
                    var productsArray = jsonObject["products"];
                    var result = JsonConvert.DeserializeObject<List<ProductDto>>(productsArray.ToString());
                    return ServiceStatus.SuccessObjectResult<List<ProductDto>>(result);
                }
                else
                {
                    return ServiceStatus.ErrorResult<List<ProductDto>>(
                        Error.Create((int)send.StatusCode, 0, "Something went wrong")
                    );
                }
            }
            catch (Exception e)
            {
                logger.LogError($"error : {e.Message}, {e.StackTrace}");
                return ServiceStatus.ErrorResult<List<ProductDto>>();
            }
        }
        public async Task<ServiceStatus<ProductDto>> GetProductAsync(int id)
        {
            try
            {
                var url = new Uri($"/products/{id}", UriKind.Relative);

                var request = new HttpRequestMessage()
                {
                    RequestUri = url,
                    Method = HttpMethod.Get
                };

                var send = await client.SendAsync(request);
                var content = await send.Content.ReadAsStringAsync();

                if (send.IsSuccessStatusCode)
                {
                    var product = JsonConvert.DeserializeObject<ProductDto>(content);
                    return ServiceStatus.SuccessObjectResult(product);
                }
                else
                {
                    return ServiceStatus.ErrorResult<ProductDto>(
                        Error.Create((int)send.StatusCode, 0, "Failed to retrieve product details")
                    );
                }
            }
            catch (Exception e)
            {
                logger.LogError($"error : {e.Message}, {e.StackTrace}");
                return ServiceStatus.ErrorResult<ProductDto>();
            }
        }
    }
}
