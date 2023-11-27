using BlazorProducts.Server.Context;
using BlazorProducts.Server.MigrationManager;
using BlazorProducts.Server.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
	.WithExposedHeaders("X-Pagination"));
});

builder.Services.AddDbContext<ProductContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),
                @"StaticFiles")),
    RequestPath = new PathString("/StaticFiles")
});

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();
