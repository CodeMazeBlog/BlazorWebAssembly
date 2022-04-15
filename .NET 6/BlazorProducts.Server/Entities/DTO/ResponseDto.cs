using System.Collections.Generic;

namespace Entities.DTO
{
    public class ResponseDto
	{
		public bool IsSuccessfulRegistration { get; set; }
		public IEnumerable<string>? Errors { get; set; }
	}
}
