using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace ShoppingCartAPI.Data;

[Index(nameof(Name))]
public class Product
{

    [JsonIgnore]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [JsonIgnore]
    public double Cost{ get; set; }
    
    [JsonIgnore]
    public double Revenue{ get; set; }
    
    [JsonIgnore]
    public double Tax{ get; set; }
    
    [JsonIgnore]
    public ICollection<ShoppingCartProduct>? ShoppingCartProducts { get; set; }

    public double FinalPrice => CalculateFinalPrice();
    public Product(int id, string name, double cost, double revenue, double tax)
    {
        Id = id;
        Name = name;
        Cost = cost;
        Revenue = revenue;
        Tax = tax;
    }

    public double CalculateFinalPrice()
    {
        var pricePerUnit = Math.Round(Cost * (Revenue + 1), 2, MidpointRounding.ToPositiveInfinity);
        return Math.Round(pricePerUnit * (Tax + 1), 2, MidpointRounding.ToPositiveInfinity);
    }
}