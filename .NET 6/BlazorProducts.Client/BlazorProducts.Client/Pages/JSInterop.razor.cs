using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorProducts.Client.Pages
{
    public partial class JSInterop
	{
		[Inject]
		public IJSRuntime? JSRuntime { get; set; }

		private IJSObjectReference? _jsModule;
		private string? _registrationResult;
		private string? _messageDetails;
		private ElementReference _elRef;
		private EmailDetails _emailDetails = new EmailDetails();
		private string? _errorMessage;

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
			{
				_jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import",
				"./scripts/jsExamples.js");

				await FocusAndStyleElement();
				await ThrowError();
			}			
		}

		public async Task ShowAlertWindow()
		{
			await _jsModule.InvokeVoidAsync("showAlertObject", new { Name = "John", Age = 35 });
		}

		private async Task RegisterEmail() =>
			_registrationResult = await _jsModule.InvokeAsync<string>("emailRegistration",
				"Please provide your email");

		private async Task ExtractEmailDetails()
		{
			var emailDetails = await _jsModule.InvokeAsync<EmailDetails>("splitEmailDetails",
				"Please provide your email");

			if (emailDetails != null)
				_messageDetails = $"Name: {emailDetails.Name}, Server: {emailDetails.Server}, " +
					$"Domain: {emailDetails.Domain}";
			else
				_messageDetails = "Email is not provided";
		}

		private async Task FocusAndStyleElement() =>
			await _jsModule.InvokeVoidAsync("focusAndStyleElement", _elRef);

		private async Task FocusAndStyleInputComponent() =>
			await _jsModule.InvokeVoidAsync("focusAndStyleInputComponent",
				"dummyInputComponent");

		private async Task ThrowError()
		{
			try
			{
				await _jsModule.InvokeVoidAsync("throwError");
			}
			catch (JSException ex)
			{
				_errorMessage = ex.Message;
				StateHasChanged();
			}
		}
			
	}

	public class EmailDetails
	{
		public string? Name { get; set; }
		public string? Server { get; set; }
		public string? Domain { get; set; }
	}
}
