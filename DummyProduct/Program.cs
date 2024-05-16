using DummyProduct.Data;
using DummyProduct.Services.Cart;
using DummyProduct.Services.DummyApi;
using DummyProduct.Services.Product;
using DummyProduct.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Detail", "/detail/{id}");
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DummyProductDb")));

builder.Services.AddHttpClient<IDummyApiService, DummyApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("DummyAPI:BaseUrl").Value);
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ProductViewModel>();
builder.Services.AddScoped<ProductDetailViewModel>();
builder.Services.AddScoped<CartViewModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
