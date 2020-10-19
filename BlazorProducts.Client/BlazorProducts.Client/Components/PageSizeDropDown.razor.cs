using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Components
{
	public partial class PageSizeDropDown
	{
		[Parameter]
		public EventCallback<int> SelectedPageSize { get; set; }

		private async Task OnPageSizeChange(ChangeEventArgs eventArgs)
		{
			await SelectedPageSize.InvokeAsync(Int32.Parse(eventArgs.Value.ToString()));
		}
	}
}
