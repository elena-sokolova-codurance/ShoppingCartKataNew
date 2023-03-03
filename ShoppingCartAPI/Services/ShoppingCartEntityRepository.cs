using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Data;

namespace ShoppingCartAPI.Services;

public class ShoppingCartEntityRepository : IShoppingCartRepository
{
    private readonly MyDbContext _context;

    public ShoppingCartEntityRepository(MyDbContext context)
    {
        _context = context;
    }
    public ShoppingCart? GetById(int id)
    {
        return _context.ShoppingCarts
            .AsNoTracking()
            .SingleOrDefault(sc => sc.Id == id);
    }

    public void AddProduct(int shoppingCartId, string productName)
    {
        var shoppingCartToUpdate = _context.ShoppingCarts.Find(shoppingCartId);
        var productToAdd = _context.Products
            .AsNoTracking()
            .SingleOrDefault(p => p.Name == productName);

        if (shoppingCartToUpdate is null || productToAdd is null)
        {
            throw new InvalidOperationException("Cart or Product does not exist");
        }

        var shoppingCartProductToAdd = _context.ShoppingCartProducts
            .Find(shoppingCartId, productToAdd.Id);

        
        if (shoppingCartProductToAdd is null)
        {
            shoppingCartProductToAdd = new ShoppingCartProduct{
                ProductId = productToAdd.Id,
                ShoppingCartId = shoppingCartId,
                // Product = productToAdd,
                // ShoppingCart = shoppingCartToUpdate
            };
            _context.ShoppingCartProducts.Add(shoppingCartProductToAdd);
        } 

        ++shoppingCartProductToAdd.Quantity;
        _context.SaveChanges();
    }

    public IEnumerable<ShoppingCartProduct> GetShoppingCartProducts(int shoppingCartId)
    {
        return _context.ShoppingCartProducts
            .Where(scp => scp.ShoppingCartId == shoppingCartId)
            .Include(scp => scp.Product)
            .AsNoTracking()
            
            .ToList();
    }
}