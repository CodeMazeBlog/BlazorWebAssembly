using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorProducts.Client.HttpRepository
{
    public class RefreshTokenService
	{
		private readonly AuthenticationStateProvider _authStateProvider;
		private readonly IAuthenticationService _authService;

		public RefreshTokenService(AuthenticationStateProvider authStateProvider,
			IAuthenticationService authService)
		{
			_authStateProvider = authStateProvider;
			_authService = authService;
		}

		public async Task<string> TryRefreshToken()
		{
			var authState = await _authStateProvider.GetAuthenticationStateAsync();
			var user = authState.User;
			var expClaim = user.FindFirst(c => c.Type.Equals("exp")).Value;
			var expTime = DateTimeOffset.FromUnixTimeSeconds(
				Convert.ToInt64(expClaim));

			var diff = expTime - DateTime.UtcNow;
			if (diff.TotalMinutes <= 2)
				return await _authService.RefreshToken();

			return string.Empty;
		}
	}
}
