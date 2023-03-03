using System.Globalization;
using ShoppingCartAPI;
using ShoppingCartAPI.Services;

namespace ShoppingCartAPI;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }

    public ShoppingCartDTO GetShoppingCart()
    {
        var shoppingCartProducts = _shoppingCartRepository.GetShoppingCartProducts(1);

        
        var shoppingCartDto = new ShoppingCartDTO(shoppingCartProducts);

        return shoppingCartDto;
    }


    public void Add(string productName)
    {
        _shoppingCartRepository.AddProduct(1, productName);
    }
}