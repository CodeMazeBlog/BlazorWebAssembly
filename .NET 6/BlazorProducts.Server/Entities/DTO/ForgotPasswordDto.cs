using System.ComponentModel.DataAnnotations;

namespace Entities.DTO
{
    public class ForgotPasswordDto
	{
		[Required]
		[EmailAddress]
		public string? Email { get; set; }
		public string? ClientURI { get; set; }
	}
}
