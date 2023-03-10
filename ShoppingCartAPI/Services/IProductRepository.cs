using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Services;

public interface IProductRepository
{
    public Product? GetByName(string name);

    public IEnumerable<Product> GetAll();
}