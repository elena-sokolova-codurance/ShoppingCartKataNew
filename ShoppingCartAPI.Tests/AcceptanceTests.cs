

using ShoppingCartAPI.Data;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI.Tests;

public class AcceptanceTests
{
    private readonly IProductRepository _productRepository = new InMemoryProductRepository();
    private readonly IShoppingCartRepository _shoppingCartRepository = new InMemoryShoppingCartRepository();
    private readonly IShoppingCartService _shoppingCartService;

    public AcceptanceTests()
    {
        _shoppingCartService = new ShoppingCartService(_shoppingCartRepository);
    }
    
    [Fact(DisplayName = "As customer I want to see my shipping empty cart")]
    public void EmptyCart()
    {
        var actualEmptyCart = _shoppingCartService.GetShoppingCart();
        actualEmptyCart.Should().BeEquivalentTo(new ShoppingCartDTO(Enumerable.Empty<ShoppingCartProduct>()));
    }
    
    [Fact(DisplayName = "Add product to shopping card")]
    public void AddProductToCart()
    {
        _shoppingCartService.Add("Iceberg");

        var actualProductCart = _shoppingCartService.GetShoppingCart();
        
        actualProductCart.ShoppingCartProducts!.Count().Should().Be(1);
        actualProductCart.TotalQuantity.Should().Be(1);
        actualProductCart.TotalPrice.Should().Be("2.17 €");
    }
    
    [Fact(DisplayName = "Add 2 similar products to shopping card")]
    public void Add2SimilarProductsToCart()
    {
        _shoppingCartService.Add("Iceberg");
        _shoppingCartService.Add("Iceberg");


        var actualProductCart = _shoppingCartService.GetShoppingCart();
        actualProductCart.ShoppingCartProducts!.Count().Should().Be(1);
        actualProductCart.TotalQuantity.Should().Be(2);
        actualProductCart.TotalPrice.Should().Be("4.34 €");
    }
    
    [Fact(DisplayName = "Add 2 similar and 1 different products to shopping card")]
    public void Add3ProductsToCart()
    {
        _shoppingCartService.Add("Iceberg");
        _shoppingCartService.Add("Iceberg");
        _shoppingCartService.Add("Chicken");

        var actualProductCart = _shoppingCartService.GetShoppingCart();
        actualProductCart.TotalQuantity.Should().Be(3);
    }
    
    [Fact(DisplayName = "Get total price for 3 products")]
    public void GetTotalPriceFor3Products()
    {
        
        _shoppingCartService.Add("Iceberg");
        _shoppingCartService.Add("Iceberg");
        _shoppingCartService.Add("Chicken");

        var actualProductCart = _shoppingCartService.GetShoppingCart();
        actualProductCart.TotalPrice.Should().Be("6.17 €");
    }
}