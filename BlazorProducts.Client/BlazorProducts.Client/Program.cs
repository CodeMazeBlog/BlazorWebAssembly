using Blazored.LocalStorage;
using Blazored.Toast;
using BlazorProducts.Client.AuthProviders;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepository;
using BlazorProducts.Client.Toastr.Services;
using Entities.Configuration;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Toolbelt.Blazor.Extensions.DependencyInjection;

namespace BlazorProducts.Client
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

			builder.Services.AddHttpClient("ProductsAPI", (sp, cl) =>
			{
				var apiConfiguration = sp.GetRequiredService<IOptions<ApiConfiguration>>();
				cl.BaseAddress = 
					new Uri(apiConfiguration.Value.BaseAddress + "/api/");
				cl.EnableIntercept(sp);
			});

			builder.Services.AddBlazoredToast();

			builder.Services.AddScoped(
				sp => sp.GetService<IHttpClientFactory>().CreateClient("ProductsAPI"));

			builder.Services.AddHttpClientInterceptor();

			builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

			builder.Services.AddScoped<HttpInterceptorService>();

			builder.Services.Configure<ApiConfiguration>
				(builder.Configuration.GetSection("ApiConfiguration"));

			builder.Services.AddBlazorToastr();
			builder.Services.AddBlazoredLocalStorage();

			builder.Services.AddAuthorizationCore();
			builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

			builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
			builder.Services.AddScoped<RefreshTokenService>();

			await builder.Build().RunAsync();
		}
	}
}
