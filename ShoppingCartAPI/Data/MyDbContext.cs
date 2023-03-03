using Microsoft.EntityFrameworkCore;

namespace ShoppingCartAPI.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Primary keys
        builder.Entity<ShoppingCartProduct>()
            .HasKey(q => new
            {
                q.ShoppingCartId, q.ProductId
            });
        
        // Relationships
        builder.Entity<ShoppingCartProduct>()
            .HasOne<ShoppingCart>(t => t.ShoppingCart)
            .WithMany(t => t.ShoppingCartProducts)
            .HasForeignKey(t => t.ShoppingCartId);
        builder.Entity<ShoppingCartProduct>()
            .HasOne<Product>(t => t.Product)
            .WithMany(t => t.ShoppingCartProducts)
            .HasForeignKey(t => t.ProductId);
    }

    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ShoppingCartProduct> ShoppingCartProducts => Set<ShoppingCartProduct>();

}