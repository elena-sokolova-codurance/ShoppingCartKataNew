using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI;
using ShoppingCartAPI.Data;
using ShoppingCartAPI.Services;


var ShoppingCartCORSPolicy = "_ShoppingCartCORSPolicy";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: ShoppingCartCORSPolicy,
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<MyDbContext>(options =>
{

    //var hostname = Environment.GetEnvironmentVariable("RDS_HOSTNAME");
    //var connectionString = $"Host={hostname};Username=academyadmin;Password=shWWq44NEEVuSLh;Database=ebdb";

    var connectionString = "Host=localhost;Username=postgres;Password=5432;Database=postgres";
    options.UseNpgsql(connectionString);
});
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartEntityRepository>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();

builder.Services.AddScoped<IProductRepository, ProductEntityRepository>();
builder.Services.AddScoped<ProductService>();


var app = builder.Build();

app.CreateDbIfNotExists();

app.MapGet("/", () => "Hello World!");


app.MapGet("/products", (ProductService productService) =>
{
    var availableProducts = productService.GetAvailableProducts();
    var options = new JsonSerializerOptions { WriteIndented = true };
    return JsonSerializer.Serialize(availableProducts, options);
});

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
app.UseSwagger();
app.UseSwaggerUI();


app.UseCors(ShoppingCartCORSPolicy);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

