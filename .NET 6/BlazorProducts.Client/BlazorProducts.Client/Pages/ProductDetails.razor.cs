using BlazorProducts.Client.HttpRepository;
using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class ProductDetails
	{
		public Product Product { get; set; } = new Product();

		[Inject]
		public IProductHttpRepository? ProductRepo { get; set; }

		[Parameter]
		public Guid ProductId { get; set; }

		protected async override Task OnInitializedAsync()
		{
			Product = await ProductRepo.GetProduct(ProductId);
		}
	}
}
