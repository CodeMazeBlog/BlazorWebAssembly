using BlazorProducts.Client.Toastr.CustomConverters;
using BlazorProducts.Client.Toastr.Enumerations;
using System.Text.Json.Serialization;

namespace BlazorProducts.Client.Toastr
{
    public class ToastrOptions
	{
		[JsonConverter(typeof(CustomEnumDescriptionConverter<ToastrPosition>))]
		[JsonPropertyName("positionClass")]
		public ToastrPosition Position { get; set; }

		[JsonConverter(typeof(CustomEnumDescriptionConverter<ToastrHideMethod>))]
		[JsonPropertyName("hideMethod")]
		public ToastrHideMethod HideMethod { get; set; }

		[JsonConverter(typeof(CustomEnumDescriptionConverter<ToastrShowMethod>))]
		[JsonPropertyName("showMethod")]
		public ToastrShowMethod ShowMethod { get; set; }

		[JsonPropertyName("closeButton")]
		public bool CloseButton { get; set; }

		[JsonPropertyName("hideDuration")]
		public int HideDuration { get; set; }
	}
}
