namespace ShoppingCartAPI.Data;

public class ShoppingCart
{
    public int Id { get; set; }
    public ICollection<ShoppingCartProduct>? ShoppingCartProducts { get; set; }
}