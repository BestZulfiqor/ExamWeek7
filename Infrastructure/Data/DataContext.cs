using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<StockAdjustment> StockAdjustments { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(n => n.Sales)
            .WithOne(n => n.Product)
            .HasForeignKey(n => n.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasMany(n => n.StockAdjustments)
            .WithOne(n => n.Product)
            .HasForeignKey(n => n.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
                    .HasOne(n => n.Category)
                    .WithMany(n => n.Products)
                    .HasForeignKey(n => n.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
                    .HasOne(n => n.Supplier)
                    .WithMany(n => n.Products)
                    .HasForeignKey(n => n.SupplierId)
                    .OnDelete(DeleteBehavior.Restrict);
    }
}