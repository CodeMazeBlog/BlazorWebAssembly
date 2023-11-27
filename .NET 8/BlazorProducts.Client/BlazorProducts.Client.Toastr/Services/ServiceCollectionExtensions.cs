using Microsoft.Extensions.DependencyInjection;

namespace BlazorProducts.Client.Toastr.Services
{
    public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlazorToastr(this IServiceCollection services)
			=> services.AddScoped<ToastrService>();
	}
}
