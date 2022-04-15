using BlazorProducts.Server.Context;
using BlazorProducts.Server.Services;
using EmailService;
using Entities.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace BlazorProducts.Server.Controllers
{
    [Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly UserManager<User> _userManager;
		private readonly IAuthenticationService _authenticationService;
		private readonly IEmailSender _emailSender;

		public AccountController(UserManager<User> userManager,
			IAuthenticationService authenticationService,
			IEmailSender emailSender)
		{
			_userManager = userManager;
			_authenticationService = authenticationService;
			_emailSender = emailSender;
		}

		[HttpPost("register")]
		public async Task<IActionResult> RegisterUser(
			[FromBody] UserForRegistrationDto userForRegistrationDto)
		{
			if (userForRegistrationDto == null || !ModelState.IsValid)
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

			await _userManager.SetTwoFactorEnabledAsync(user, true);
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			var param = new Dictionary<string, string>
			{
				{ "token", token },
				{ "email", userForRegistrationDto.Email }
			};

			var callback = QueryHelpers.AddQueryString(userForRegistrationDto.ClientURI, param);

			var message = new Message(new string[] { user.Email }, "Email Confirmation token",
				callback, null);

			await _emailSender.SendEmailAsync(message);

			await _userManager.AddToRoleAsync(user, "Viewer");

			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(
			[FromBody] UserForAuthenticationDto userForAuthenticationDto)
		{
			var user = await _userManager.FindByNameAsync(userForAuthenticationDto.Email);

			if (user == null)
			{
				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "Invalid Request"
				});
			}

			if (!await _userManager.IsEmailConfirmedAsync(user))
				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "Email is not confirmed"
				});

			if (await _userManager.IsLockedOutAsync(user))
				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "The account is locked out"
				});

			if (!await _userManager.CheckPasswordAsync(user, userForAuthenticationDto.Password))
			{
				await _userManager.AccessFailedAsync(user);

				if (await _userManager.IsLockedOutAsync(user))
				{
					var content = $"Your account is locked out. " +
						$"If you want to reset the password, you can use the " +
						$"Forgot Password link on the Login page";

					var message = new Message(new string[] { userForAuthenticationDto.Email },
						"Locked out account information", content, null);

					await _emailSender.SendEmailAsync(message);

					return Unauthorized(new AuthResponseDto
					{
						ErrorMessage = "The account is locked out"
					});
				}

				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "Invalid Authentication"
				});
			}

			if (await _userManager.GetTwoFactorEnabledAsync(user))
				return await GenerateOTPFor2StepVerification(user);

			var token = await _authenticationService.GetToken(user);

			user.RefreshToken = _authenticationService.GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
			await _userManager.UpdateAsync(user);

			await _userManager.ResetAccessFailedCountAsync(user);

			return Ok(new AuthResponseDto
			{
				IsAuthSuccessful = true,
				Token = token,
				RefreshToken = user.RefreshToken
			});
		}

		private async Task<IActionResult> GenerateOTPFor2StepVerification(User user)
		{
			var providers = await _userManager.GetValidTwoFactorProvidersAsync(user);
			if (!providers.Contains("Email"))
			{
				return Unauthorized(new AuthResponseDto
				{
					ErrorMessage = "Invalid 2-Step Verification Provider"
				});
			}

			var token = await _userManager.GenerateTwoFactorTokenAsync(user, "Email");

			var message = new Message(new string[] { user.Email }, "Authentication token",
				token, null);

			await _emailSender.SendEmailAsync(message);

			return Ok(new AuthResponseDto
			{
				Is2StepVerificationRequired = true,
				Provider = "Email"
			});
		}

		[HttpPost("ForgotPassword")]
		public async Task<IActionResult> ForgotPassword(
			[FromBody] ForgotPasswordDto forgotPasswordDto)
		{
			var user = await _userManager.FindByEmailAsync(forgotPasswordDto.Email);
			if (user == null)
				return BadRequest("Invalid Request");

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			var param = new Dictionary<string, string>
			{
				{ "token", token },
				{ "email", forgotPasswordDto.Email }
			};

			var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);

			var message = new Message(new string[] { user.Email }, "Reset password token",
				callback, null);

			await _emailSender.SendEmailAsync(message);

			return Ok();
		}

		[HttpPost("ResetPassword")]
		public async Task<IActionResult> ResetPassword(
			[FromBody] ResetPasswordDto resetPasswordDto)
		{
			var errorResponse = new ResetPasswordResponseDto
			{
				Errors = new string[] { "Reset Password Failed" }
			};

			if (!ModelState.IsValid)
				return BadRequest(errorResponse);

			var user = await _userManager.FindByEmailAsync(resetPasswordDto.Email);
			if (user == null)
				return BadRequest(errorResponse);

			var resetPassResult = await _userManager.ResetPasswordAsync(user,
				resetPasswordDto.Token, resetPasswordDto.Password);

			if (!resetPassResult.Succeeded)
			{
				var errors = resetPassResult.Errors.Select(e => e.Description);
				return BadRequest(new ResetPasswordResponseDto { Errors = errors });
			}

			await _userManager.SetLockoutEndDateAsync(user, null);

			return Ok(new ResetPasswordResponseDto { IsResetPasswordSuccessful = true });
		}

		[HttpGet("EmailConfirmation")]
		public async Task<IActionResult> EmailConfirmation([FromQuery] string email,
			[FromQuery] string token)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
				return BadRequest();

			var confirmResult = await _userManager.ConfirmEmailAsync(user, token);
			if (!confirmResult.Succeeded)
				return BadRequest();

			return Ok();
		}

		[HttpPost("TwoStepVerification")]
		public async Task<IActionResult> TwoStepVerification(
			[FromBody] TwoFactorVerificationDto twoFactorVerificationDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(new AuthResponseDto
				{
					ErrorMessage = "Invalid Request"
				});

			var user = await _userManager.FindByEmailAsync(twoFactorVerificationDto.Email);
			if (user == null)
				return BadRequest(new AuthResponseDto
				{
					ErrorMessage = "Invalid Request"
				});

			var validVerification = await _userManager.VerifyTwoFactorTokenAsync(user,
				twoFactorVerificationDto.Provider, twoFactorVerificationDto.TwoFactorToken);
			if (!validVerification)
				return BadRequest(new AuthResponseDto
				{
					ErrorMessage = "Invalid Token Verification"
				});

			var token = await _authenticationService.GetToken(user);
			user.RefreshToken = _authenticationService.GenerateRefreshToken();
			user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
			await _userManager.UpdateAsync(user);
			await _userManager.ResetAccessFailedCountAsync(user);

			return Ok(new AuthResponseDto
			{
				IsAuthSuccessful = true,
				Token = token,
				RefreshToken = user.RefreshToken
			});
		}
	}
}
