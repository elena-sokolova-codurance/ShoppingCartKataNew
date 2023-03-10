using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Tests;

public class ProductShould
{
    [Fact]
    public void CalculateFinalPrice()
    {
        var product = new Product(1,"Iceberg", 1.55, 0.15, 0.21);
        product.CalculateFinalPrice().Should().Be(2.17);
    }
}