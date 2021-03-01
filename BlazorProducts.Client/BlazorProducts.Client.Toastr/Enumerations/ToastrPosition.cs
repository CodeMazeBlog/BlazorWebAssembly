using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Toastr.Enumerations
{
	public enum ToastrPosition
	{
		[Description("toast-top-left")] TopLeft,
		[Description("toast-top-right")] TopRight,
		[Description("toast-bottom-left")] BottomLeft,
		[Description("toast-bottom-right")] BottomRight
	}

}
