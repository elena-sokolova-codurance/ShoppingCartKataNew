using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MyDbContext>(options =>
{

    var hostname = Environment.GetEnvironmentVariable("RDS_HOSTNAME");
    var connectionString = $"Host={hostname};Username=academyadmin;Password=shWWq44NEEVuSLh;Database=ebdb";

    //var connectionString = "Host=localhost;Username=postgres;Password=123456;Database=postgres";
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartEntityRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();


var app = builder.Build();

app.CreateDbIfNotExists();

app.MapGet("/", () => "Hello World!");
app.MapGet("/host-name", () => Environment.GetEnvironmentVariable("RDS_HOSTNAME"));

app.MapPost("/add-item", (IShoppingCartService shoppingCartService, ItemRequest request) =>
{
    shoppingCartService.Add(request.ProductName);
    return $"Item {request.ProductName} added";
});

app.MapGet("/shopping-cart", (IShoppingCartService shoppingCartService) =>
{
    var shoppingCart = shoppingCartService.GetShoppingCart();
    var options = new JsonSerializerOptions { WriteIndented = true };
    return JsonSerializer.Serialize(shoppingCart, options);
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

