using BlazorProducts.Client.Features;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorProducts.Client.HttpRepository
{
    public class ProductHttpRepository : IProductHttpRepository
	{
		private readonly HttpClient _client;
		private readonly JsonSerializerOptions _options =
			new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		public ProductHttpRepository(HttpClient client)
		{
			_client = client;
		}

		public async Task<Product> GetProduct(Guid id)
		{
			var product = await _client.GetFromJsonAsync<Product>($"products/{id}");

			return product;
		}

		public async Task<PagingResponse<Product>> GetProducts(ProductParameters productParameters)
		{
			var queryStringParam = new Dictionary<string, string>
			{
				["pageNumber"] = productParameters.PageNumber.ToString(),
				["pageSize"] = productParameters.PageSize.ToString(),
				["searchTerm"] = productParameters.SearchTerm == null ? string.Empty : productParameters.SearchTerm,
				["orderBy"] = productParameters.OrderBy == null ? "" : productParameters.OrderBy
			};

			var response =
				await _client.GetAsync(QueryHelpers.AddQueryString("products", queryStringParam));

			var content = await response.Content.ReadAsStringAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new ApplicationException(content);
			}

			var pagingResponse = new PagingResponse<Product>
			{
				Items = JsonSerializer.Deserialize<List<Product>>(content, _options),
				MetaData = JsonSerializer.Deserialize<MetaData>(
					response.Headers.GetValues("X-Pagination").First(), _options)
			};

			return pagingResponse;
		}
	}
}
