using Microsoft.EntityFrameworkCore;
using ShoppingApp.Models;

namespace ShoppingApp.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Color> Colors { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<ContactU> ContactUs { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductID);
                
            modelBuilder.Entity<Product>()
                .HasOne<Category>()
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne<Color>()
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ColorId);

            modelBuilder.Entity<Product>()
                .HasOne<Model>()
                .WithMany(m => m.Products)
                .HasForeignKey(p => p.ModelId);

            modelBuilder.Entity<Product>()
                .HasOne<User>()
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.UserId);
        }
    }
}