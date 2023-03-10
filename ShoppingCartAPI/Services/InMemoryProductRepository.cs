using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Services;

public class InMemoryProductRepository : IProductRepository
{
    private Dictionary<string, Product> _products = new();

    public InMemoryProductRepository()
    {
        _products.Add("Iceberg", new Product(1,"Iceberg", 1.55, 0.15, 0.21));
        _products.Add("Tomato", new Product(2,"Tomato", 0.52, 0.15, 0.21));
        _products.Add("Chicken", new Product(3, "Chicken", 1.34, 0.12, 0.21));
        _products.Add("Bread", new Product(4, "Bread", 0.72, 0.12, 0.10));
        _products.Add("Corn", new Product(5, "Corn", 1.21, 0.12, 0.10));
    }

    public Product? GetByName(string name)
    {
        if (_products.TryGetValue(name, out var product))
        {
            return product;
        }
        return null;
    }

    public IEnumerable<Product> GetAll()
    {
        return _products.Values;
    }
}