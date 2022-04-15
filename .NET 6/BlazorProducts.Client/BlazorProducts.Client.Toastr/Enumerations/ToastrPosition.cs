using System.ComponentModel;

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
