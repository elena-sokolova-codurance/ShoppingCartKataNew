using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ShoppingCartAPI.Data;

public class ShoppingCartProduct
{

    [JsonIgnore]
    public int ShoppingCartId { get; set; }
    [JsonIgnore]
    public int ProductId { get; set; }
    [JsonIgnore]
    public ShoppingCart? ShoppingCart { get; set; }
    public Product? Product { get; set; }

    public int Quantity { get; set; }
}