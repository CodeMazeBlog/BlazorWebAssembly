using Entities.Models;

namespace BlazorProducts.Client.HttpRepository
{
    public interface IProductHttpRepository
	{
		Task<List<Product>> GetProducts();
		Task<Product> GetProduct(Guid id);
	}
}
