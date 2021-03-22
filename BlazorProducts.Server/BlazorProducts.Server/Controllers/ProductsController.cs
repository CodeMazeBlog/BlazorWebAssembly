using System;
using System.Text.Json;
using System.Threading.Tasks;
using BlazorProducts.Server.Repository;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProducts.Server.Controllers
{
	[Route("api/products")]
	[ApiController]
	[Authorize]
	public class ProductsController : ControllerBase
	{
		private readonly IProductRepository _repo;

		public ProductsController(IProductRepository repo)
		{
			_repo = repo;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery] ProductParameters productParameters)
		{
			var products = await _repo.GetProducts(productParameters);

			Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(products.MetaData));

			return Ok(products);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetProduct(Guid id)
		{
			var product = await _repo.GetProduct(id);
			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProduct([FromBody] Product product)
		{
			if (product == null)
				return BadRequest("Product has not been set");

			//model validation

			await _repo.CreateProduct(product);

			return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] Product product)
		{
			var dbProduct = await _repo.GetProduct(id);
			if (dbProduct == null)
				return NotFound();

			await _repo.UpdateProduct(product, dbProduct);

			return NoContent();
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(Guid id)
		{
			var product = await _repo.GetProduct(id);
			if (product == null)
				return NotFound();

			await _repo.DeleteProduct(product);

			return NoContent();
		}
	}
}