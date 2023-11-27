using BlazorProducts.Client.HttpRepository;
using Entities.DTO;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorProducts.Client.Pages
{
    public partial class TwoStepVerification
	{
		private TwoFactorVerificationDto _twoFactorDto = new TwoFactorVerificationDto();
		private bool _showError;
		private string? _error;

		[Inject]
		public IAuthenticationService? AuthService { get; set; }
		[Inject]
		public NavigationManager? NavigationManager { get; set; }

		[SupplyParameterFromQuery]
		[Parameter]
        public string? Email { get; set; }

		[SupplyParameterFromQuery]
		[Parameter]
		public string? Provider { get; set; }

		protected override void OnInitialized()
		{
			if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Provider))
			{
				NavigationManager.NavigateTo("/");
			}
			else
            {
				_twoFactorDto.Email = Email;
				_twoFactorDto.Provider = Provider;
            }
		}

		private async Task Submit()
		{
			_showError = false;

			var result = await AuthService.LoginVerification(_twoFactorDto);
			if (result.IsAuthSuccessful)
				NavigationManager.NavigateTo("/");
			else
			{
				_error = result.ErrorMessage;
				_showError = true;
			}
		}
	}
}
