using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.DTO
{
	public class ForgotPasswordDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		public string ClientURI { get; set; }
	}
}
