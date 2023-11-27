using BlazorProducts.Server.Context;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorProducts.Server.Services
{
    public interface IAuthenticationService
	{
		Task<string> GetToken(User user);
		string GenerateRefreshToken();
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
