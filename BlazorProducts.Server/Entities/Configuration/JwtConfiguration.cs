using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Configuration
{
	public class JwtConfiguration
	{
		public string SecurityKey { get; set; }
		public string ValidIssuer { get; set; }
		public string ValidAudience { get; set; }
		public int ExpiryInMinutes { get; set; }
	}
}
