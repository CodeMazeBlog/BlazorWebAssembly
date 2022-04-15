using Microsoft.JSInterop;

namespace BlazorProducts.Client.Toastr.Services
{
    public class ToastrService
	{
		private IJSRuntime _jsRuntime;

		public ToastrService(IJSRuntime jSRuntime)
		{
			_jsRuntime = jSRuntime;
		}

		public async Task ShowInfoMessage(string message, object options)
		{
			await _jsRuntime.InvokeVoidAsync("toastrWrapper.showToastrInfo", 
				message, options);
		}
	}
}
