using BlazorProducts.Client.Toastr;
using BlazorProducts.Client.Toastr.Enumerations;
using BlazorProducts.Client.Toastr.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorProducts.Client.Pages
{
    public partial class ToastrWrapper
	{
		[Inject]
		public ToastrService? ToastrService { get; set; }

		private async Task ShowToastrInfo()
		{
			var message = "This is a message sent from the C# method.";

			var options = new ToastrOptions
			{
				CloseButton = true,
				HideDuration = 300,
				HideMethod = ToastrHideMethod.SlideUp,
				ShowMethod = ToastrShowMethod.SlideDown,
				Position = ToastrPosition.TopRight
			};
			await ToastrService.ShowInfoMessage(message, options);
		}
	}
}
