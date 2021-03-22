using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTO
{
	public class ResponseDto
	{
		public bool IsSuccessfulRegistration { get; set; }
		public IEnumerable<string> Errors { get; set; }
	}
}
