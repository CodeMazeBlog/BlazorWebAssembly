using Entities.Models;
using System.Net.Http.Json;

namespace BlazorProducts.Client.HttpRepository
{
    public class ProductHttpRepository : IProductHttpRepository
	{
		private readonly HttpClient _client;

		public ProductHttpRepository(HttpClient client)
		{
			_client = client;
		}

		public async Task<Product> GetProduct(Guid id)
		{
			var product = await _client.GetFromJsonAsync<Product>($"products/{id}");

			return product;
		}

		public async Task<List<Product>> GetProducts()
		{
			var products =
				await _client.GetFromJsonAsync<List<Product>>("products");

			return products;
		}
	}
}
