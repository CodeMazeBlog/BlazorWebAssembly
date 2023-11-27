using BlazorProducts.Client.Features;
using Entities.Models;
using Entities.RequestFeatures;

namespace BlazorProducts.Client.HttpRepository
{
    public interface IProductHttpRepository
	{
		Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters);
		Task<Product> GetProduct(Guid id);
		Task CreateProduct(Product product);
		Task<string> UploadProductImage(MultipartFormDataContent content);
	}
}
