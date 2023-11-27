using BlazorProducts.Server.Context;
using BlazorProducts.Server.Services;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IAuthenticationService _authenticationService;

		public AccountController(UserManager<User> userManager,
			IAuthenticationService authenticationService)
		{
			_userManager = userManager;
			_authenticationService = authenticationService;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser(
			[FromBody] UserForRegistrationDto userForRegistrationDto)
		{
			if (userForRegistrationDto is null || !ModelState.IsValid)
				return BadRequest();

			var user = new User
			{
				UserName = userForRegistrationDto.Email,
				Email = userForRegistrationDto.Email
			};

			var result = await _userManager.CreateAsync(user, userForRegistrationDto.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(e => e.Description);
				return BadRequest(new ResponseDto { Errors = errors });
			}

			await _userManager.AddToRoleAsync(user, "Viewer");

			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(
			[FromBody] UserForAuthenticationDto userForAuthenticationDto)
		{
			var user = await _userManager.FindByNameAsync(userForAuthenticationDto.Email);

			if (user is null || !await _userManager.CheckPasswordAsync(user,
				userForAuthenticationDto.Password))
			{
				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "Invalid Authentication"
				});
			}

			var token = await _authenticationService.GetToken(user);

			user.RefreshToken = _authenticationService.GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
			await _userManager.UpdateAsync(user);

			return Ok(new AuthResponseDto
			{
				IsAuthSuccessful = true,
				Token = token,
				RefreshToken = user.RefreshToken
			});
		}
	}
}
