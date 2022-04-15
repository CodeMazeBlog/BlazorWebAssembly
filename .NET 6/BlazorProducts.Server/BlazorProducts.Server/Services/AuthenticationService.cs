using BlazorProducts.Server.Context;
using Entities.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BlazorProducts.Server.Services
{
    public class AuthenticationService : IAuthenticationService
	{
		private readonly JwtConfiguration _jwtSettings;
		private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
		private readonly UserManager<User> _userManager;

		public AuthenticationService(IOptions<JwtConfiguration> jwtSettings,
			UserManager<User> userManager)
		{
			_jwtSettings = jwtSettings.Value;
			_jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			_userManager = userManager;
		}

		public async Task<string> GetToken(User user)
		{
			var signingCredentials = GetSigningCredentials();

			var claims = await GetClaims(user);

			var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

			return _jwtSecurityTokenHandler.WriteToken(tokenOptions);
		}

		private SigningCredentials GetSigningCredentials()
		{
			var key = Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey);
			var secret = new SymmetricSecurityKey(key);

			return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
		}

		private async Task<IEnumerable<Claim>> GetClaims(User user)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Email)
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			return claims;
		}

		private JwtSecurityToken GenerateTokenOptions
			(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
		{
			var tokenOptions = new JwtSecurityToken(
				issuer: _jwtSettings.ValidIssuer,
				audience: _jwtSettings.ValidAudience,
				claims: claims,
				expires: DateTime.Now.AddMinutes(Convert.ToDouble
					(_jwtSettings.ExpiryInMinutes)),
				signingCredentials: signingCredentials);

			return tokenOptions;
		}

		public string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(randomNumber);
				return Convert.ToBase64String(randomNumber);
			}
		}

		public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
		{
			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateAudience = true,
				ValidateIssuer = true,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey)),
				ValidateLifetime = false,
				ValidIssuer = _jwtSettings.ValidIssuer,
				ValidAudience = _jwtSettings.ValidAudience,
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			SecurityToken securityToken;

			var principal = tokenHandler.ValidateToken(token, 
				tokenValidationParameters, out securityToken);

			var jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || 
				!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
				StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}

			return principal;
		}
	}
}
