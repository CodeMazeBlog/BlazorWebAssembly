using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class ResetPasswordDto
	{
		[Required(ErrorMessage = "Password is required")]
		public string? Password { get; set; }
		[Compare(nameof(Password), 
			ErrorMessage = "The password and confirmation password do not match.")]
		public string? ConfirmPassword { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
	}
}
