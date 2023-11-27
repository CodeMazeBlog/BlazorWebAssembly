using BlazorProducts.Server.Context;
using BlazorProducts.Server.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/token")]
	[ApiController]
	public class TokenController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IAuthenticationService _authenticationService;

		public TokenController(UserManager<User> userManager,
			IAuthenticationService authenticationService)
		{
			_userManager = userManager;
			_authenticationService = authenticationService;
		}

		[HttpPost("refresh")]
		public async Task<IActionResult> Refresh(
			[FromBody] RefreshTokenDto tokenDto)
		{
			if (tokenDto == null)
				return BadRequest(new AuthResponseDto
				{
					IsAuthSuccessful = false,
					ErrorMessage = "Invalid client request"
				});

			var principal = _authenticationService
				.GetPrincipalFromExpiredToken(tokenDto.Token);
			var username = principal.Identity.Name;

			var user = await _userManager.FindByEmailAsync(username);
			if (user == null || user.RefreshToken != tokenDto.RefreshToken ||
				user.RefreshTokenExpiryTime <= DateTime.Now)
				return BadRequest(new AuthResponseDto
				{
					IsAuthSuccessful = false,
					ErrorMessage = "Invalid client request"
				});

			var token = await _authenticationService.GetToken(user);
			user.RefreshToken = _authenticationService.GenerateRefreshToken();

			await _userManager.UpdateAsync(user);

			return Ok(new AuthResponseDto
			{
				Token = token,
				RefreshToken = user.RefreshToken,
				IsAuthSuccessful = true
			});
		}
	}
}
