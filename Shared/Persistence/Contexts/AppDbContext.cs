using BackendXComponent.Shared.Extensions;
using BackendXComponent.ComponentX.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendXComponent.Shared.Persistence.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<SubProduct> SubProducts { get; set; }
        
        public DbSet<Cart> Carts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; } // Agregamos DbSet para OrderDetail

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().ToTable("Products");
            builder.Entity<Product>().HasKey(p => p.Id);
            builder.Entity<Product>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(300);
            builder.Entity<Product>().Property(p => p.Description).IsRequired().HasMaxLength(800);
            builder.Entity<Product>().Property(p => p.Image).IsRequired().HasMaxLength(800);
            builder.Entity<Product>().Property(p => p.Price).IsRequired();
            builder.Entity<Product>().Property(p => p.SpecificDetails).IsRequired().HasMaxLength(1000);
            builder.Entity<Product>().Property(p => p.GeneralDetails).IsRequired().HasMaxLength(1000);

            builder.Entity<Product>()
                .HasMany(p => p.SubProductsList)
                .WithOne(p => p.Product)
                .HasForeignKey(p => p.ProductId);

            builder.Entity<SubProduct>().ToTable("Subproducts");
            builder.Entity<SubProduct>().HasKey(p => p.Id);
            builder.Entity<SubProduct>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SubProduct>().Property(p => p.Name).IsRequired().HasMaxLength(500);
            builder.Entity<SubProduct>().Property(p => p.Specification).IsRequired().HasMaxLength(800);
            builder.Entity<SubProduct>().Property(p => p.Price).IsRequired();
            builder.Entity<SubProduct>().Property(p => p.Image).IsRequired().HasMaxLength(800);
            builder.Entity<SubProduct>()
                .HasOne(p => p.Product)
                .WithMany(p => p.SubProductsList)
                .HasForeignKey(p => p.ProductId);

            //implement cart
            builder.Entity<Cart>().ToTable("Carts");
            builder.Entity<Cart>().HasKey(p => p.Id);
            builder.Entity<Cart>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Cart>().Property(p => p.UserId).IsRequired().HasMaxLength(500);
            builder.Entity<Cart>().Property(p => p.ProductID).IsRequired().HasMaxLength(500);
            builder.Entity<Cart>().Property(p => p.Quantity).IsRequired().HasMaxLength(800);
            builder.Entity<Cart>().Property(p => p.TotalPrice).IsRequired().HasMaxLength(800);
            builder.Entity<Cart>().Property(p => p.SubproductId).IsRequired();
            // Configuración de la relación entre Cart y User (uno a uno)
            
            
            
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(p => p.Id);
            builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(p => p.FirstName).IsRequired().HasMaxLength(500);
            builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(500);
            builder.Entity<User>().Property(p => p.Email).IsRequired().HasMaxLength(800);
            builder.Entity<User>().Property(p => p.Password).IsRequired().HasMaxLength(800);
            // Configuración de la relación entre User y Cart (uno a uno)
           
            
            
            builder.Entity<Order>().ToTable("Orders");
            builder.Entity<Order>().HasKey(p => p.Id);
            builder.Entity<Order>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Order>().Property(p => p.UserId).IsRequired();
            builder.Entity<Order>().Property(p => p.Date).IsRequired().HasMaxLength(500);
            builder.Entity<Order>().Property(p => p.Status).IsRequired().HasMaxLength(500);
            // Configuración de la relación entre Order y User (uno a muchos)
            builder.Entity<Order>()
                .HasOne(p => p.User)
                .WithMany(u => u.OrdersList)
                .HasForeignKey(p => p.UserId);
            
            
            // Configuración para OrderDetail
            builder.Entity<OrderDetail>().ToTable("OrderDetails");
            builder.Entity<OrderDetail>().HasKey(p => p.Id);
            builder.Entity<OrderDetail>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<OrderDetail>().Property(p => p.OrderId).IsRequired();
            builder.Entity<OrderDetail>().Property(p => p.ProductId).IsRequired();
            builder.Entity<OrderDetail>().Property(p => p.Quantity).IsRequired();
            builder.Entity<OrderDetail>().Property(p => p.UnitPrice).IsRequired();
            builder.UseSnakeCaseNamingConvention();
        }
    }
}

