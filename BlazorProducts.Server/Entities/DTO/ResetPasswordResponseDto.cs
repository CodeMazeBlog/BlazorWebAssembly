using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
	public class ResetPasswordResponseDto
	{
		public bool IsResetPasswordSuccessful { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
