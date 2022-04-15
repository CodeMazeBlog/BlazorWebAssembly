using System.Collections.Generic;

namespace Entities.DTO
{
    public class ResetPasswordResponseDto
	{
		public bool IsResetPasswordSuccessful { get; set; }
		public IEnumerable<string>? Errors { get; set; }
	}
}
