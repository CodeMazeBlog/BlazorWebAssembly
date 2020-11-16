using BlazorProducts.Client.Shared;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Components
{
	public partial class ProductTable
	{
		[Parameter]
		public List<Product> Products { get; set; }

		[Parameter]
		public EventCallback<Guid> OnDelete { get; set; }
		
		private Confirmation _confirmation;
		private Guid _productIdToDelete;

		private void CallConfirmationModal(Guid id)
		{
			_productIdToDelete = id;
			_confirmation.Show();
		}

		private async Task DeleteProduct()
		{
			_confirmation.Hide();
			await OnDelete.InvokeAsync(_productIdToDelete);
		}
	}
}
