using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Services;

public interface IShoppingCartRepository
{
    public ShoppingCart? GetById(int id);
    public void AddProduct(int shoppingCartId, string productName);

    public IEnumerable<ShoppingCartProduct>? GetShoppingCartProducts(int shoppingCartId);
}