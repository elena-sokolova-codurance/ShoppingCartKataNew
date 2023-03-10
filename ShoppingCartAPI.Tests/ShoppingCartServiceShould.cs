using ShoppingCartAPI.Data;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI.Tests;

public class ShoppingCartServiceShould
{
    private readonly IShoppingCartService _shoppingCartService;
    private Mock<IShoppingCartRepository> _shoppingCartRepo;

    public ShoppingCartServiceShould()
    {
        _shoppingCartRepo = new Mock<IShoppingCartRepository>();
        _shoppingCartService = new ShoppingCartService(_shoppingCartRepo.Object);

    }

    [Fact(DisplayName = "Verify that ProductService GetProduct was invoked")]
    public void InvokeProductServiceGetProduct()
    {
        var shoppingCart = new Mock<ShoppingCart>();

        _shoppingCartRepo.Setup(m => 
            m.GetById(1)).Returns(shoppingCart.Object);

        _shoppingCartService.Add("Iceberg");
        
        _shoppingCartRepo.Verify(m => m.AddProduct(1, "Iceberg"));
        
    }
}