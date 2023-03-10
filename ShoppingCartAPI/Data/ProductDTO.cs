using System.Globalization;

namespace ShoppingCartAPI.Data;

public class ProductDTO
{
    public string Name { get; set; }

    public string Price { get; set; }

    public ProductDTO(string name, double price)
    {
        Name = name;
        Price = PriceToString(price);
    }

    private static string PriceToString(double totalPrice)
    {
        return Convert.ToString(totalPrice, CultureInfo.InvariantCulture).Replace(',', '.') + " €";
    }
}