using System.Globalization;
using ShoppingCartAPI.Data;

namespace ShoppingCartAPI;

public class ShoppingCartDTO
{
    public ShoppingCartDTO(IEnumerable<ShoppingCartProduct> shoppingCartProducts)
    {
        
        ShoppingCartProducts = shoppingCartProducts;

        double totalPrice = 0;
        
        foreach (var spc in shoppingCartProducts)
        {
            totalPrice += spc.Product!.CalculateFinalPrice() * spc.Quantity;

            TotalQuantity += spc.Quantity;
        }
        
        TotalPrice = PriceToString(totalPrice);

    }

    private static string PriceToString(double totalPrice)
    {
        return Convert.ToString(totalPrice, CultureInfo.InvariantCulture).Replace(',', '.') + " €";
    }
    
    public int TotalQuantity { get; set; }

    public IEnumerable<ShoppingCartProduct>? ShoppingCartProducts { get; set; } = new List<ShoppingCartProduct>();
    public string TotalPrice { get; set; } = "0 €";
}