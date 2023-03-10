using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Services;

public class ProductEntityRepository : IProductRepository
{
    private readonly MyDbContext _context;

    public ProductEntityRepository(MyDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAll()
    {
        return _context.Products
            .AsNoTracking()
            .ToList();
    }

    public Product? GetById(int id)
    {
        return _context.Products
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);
    }

    public Product? GetByName(string name)
    {
        return _context.Products
            .AsNoTracking()
            .SingleOrDefault(p => p.Name == name);
    }

    public Product Create(Product newProduct)
    {
        _context.Products.Add(newProduct);
        _context.SaveChanges();

        return newProduct;
    }

    public void Update(int productId, double cost)
    {
        var productToUpdate = _context.Products.Find(productId);

        if (productToUpdate is null)
        {
            throw new InvalidOperationException("Product does not exist");
        }

        productToUpdate.Cost = cost;

        _context.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var productToDelete = _context.Products.Find(id);
        if (productToDelete is not null)
        {
            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
        }
    }
    
}