using BlazorProducts.Client.HttpRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace BlazorProducts.Client.Pages
{
    public partial class EmailConfirmation
	{
		private bool _showError;
		private bool _showSuccess;

		[Inject]
		public IAuthenticationService? AuthService { get; set; }
		[Inject]
		public NavigationManager? NavigationManager { get; set; }

		protected async override Task OnInitializedAsync()
		{
			_showError = _showSuccess = false;

			var uri = NavigationManager.ToAbsoluteUri(NavigationManager.Uri);

			var queryStrings = QueryHelpers.ParseQuery(uri.Query);
			if (queryStrings.TryGetValue("email", out var email) &&
				queryStrings.TryGetValue("token", out var token))
			{
				var result = await AuthService.EmailConfirmation(email, token);
				if (result == HttpStatusCode.OK)
					_showSuccess = true;
				else
					_showError = true;
			}
			else
			{
				NavigationManager.NavigateTo("/");
			}
		}
	}
}
