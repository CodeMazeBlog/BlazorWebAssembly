using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorProducts.Client.Toastr.Services
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddBlazorToastr(this IServiceCollection services)
			=> services.AddScoped<ToastrService>();
	}
}
