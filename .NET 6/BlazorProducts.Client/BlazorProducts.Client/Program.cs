using Blazored.Toast;
using BlazorProducts.Client;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepository;
using BlazorProducts.Client.Toastr.Services;
using Entities.Configuration;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Options;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

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

await builder.Build().RunAsync();