namespace ShoppingCartAPI;

public interface IShoppingCartService
{
    ShoppingCartDTO GetShoppingCart();
    void Add(string productName);
}