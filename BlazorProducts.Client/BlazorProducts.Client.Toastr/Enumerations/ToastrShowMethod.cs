using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Toastr.Enumerations
{
	public enum ToastrShowMethod
	{
		[Description("fadeIn")] FadeIn,
		[Description("slideDown")] SlideDown
	}
}
