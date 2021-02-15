using BlazorProducts.Server.Context;
using BlazorProducts.Server.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace BlazorProducts.Server
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(policy =>
			{
				policy.AddPolicy("CorsPolicy", opt => opt
				.AllowAnyOrigin()
				.AllowAnyHeader()
				.AllowAnyMethod()
				.WithExposedHeaders("X-Pagination"));
			});

			services.AddDbContext<ProductContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

			services.AddScoped<IProductRepository, ProductRepository>();

			services.AddControllers();

			services.AddRazorPages();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseWebAssemblyDebugging();
			}

			app.UseHttpsRedirection();
			app.UseCors("CorsPolicy");

			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
				@"StaticFiles")),
				RequestPath = new PathString("/StaticFiles")
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapRazorPages();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
