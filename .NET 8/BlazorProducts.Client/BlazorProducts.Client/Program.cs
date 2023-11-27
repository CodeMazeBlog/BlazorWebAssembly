using BlazorProducts.Client;
using BlazorProducts.Client.HttpInterceptor;
using BlazorProducts.Client.HttpRepository;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("ProductsAPI", (sp, cl) =>
{
	cl.BaseAddress = new Uri("https://localhost:5011/api/");
	cl.EnableIntercept(sp);
});

builder.Services.AddScoped(
	sp => sp.GetService<IHttpClientFactory>().CreateClient("ProductsAPI"));

builder.Services.AddHttpClientInterceptor();
builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddScoped<IProductHttpRepository, ProductHttpRepository>();

await builder.Build().RunAsync();
