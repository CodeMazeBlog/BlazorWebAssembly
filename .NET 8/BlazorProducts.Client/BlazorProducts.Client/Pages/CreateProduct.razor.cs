using Blazored.Toast.Services;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepository;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorProducts.Client.Pages
{
    public partial class CreateProduct : IDisposable
	{
		private Product _product = new Product();
		private EditContext? _editContext;
		private bool formInvalid = true;

		[Inject]
		public IProductHttpRepository? ProductRepo { get; set; }

		[Inject]
		public HttpInterceptorService? Interceptor { get; set; }

		[Inject]
		public IToastService? ToastService { get; set; }

		protected override void OnInitialized()
		{
			_editContext = new EditContext(_product);
			_editContext.OnFieldChanged += HandleFieldChanged;
			Interceptor.RegisterEvent();
		}

		private void HandleFieldChanged(object? sender, FieldChangedEventArgs e)
		{
			formInvalid = !_editContext.Validate();
			StateHasChanged();
		}

		private async Task Create()
		{
			await ProductRepo.CreateProduct(_product);

			ToastService.ShowSuccess($"Action successful. " +
				$"Product \"{_product.Name}\" successfully added.");
			_product = new Product();
			_editContext.OnValidationStateChanged += ValidationChanged;
			_editContext.NotifyValidationStateChanged();
		}

		private void ValidationChanged(object? sender, ValidationStateChangedEventArgs e)
		{
			formInvalid = true;
			_editContext.OnFieldChanged -= HandleFieldChanged;
			_editContext = new EditContext(_product);
			_editContext.OnFieldChanged += HandleFieldChanged;
			_editContext.OnValidationStateChanged -= ValidationChanged;
		}

		private void AssignImageUrl(string imgUrl)
			=> _product.ImageUrl = imgUrl;

		public void Dispose()
		{
			Interceptor.DisposeEvent();
			_editContext.OnFieldChanged -= HandleFieldChanged;
			_editContext.OnValidationStateChanged -= ValidationChanged;
		}
	}
}
