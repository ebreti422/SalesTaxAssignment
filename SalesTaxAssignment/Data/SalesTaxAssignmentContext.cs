using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Data
{
    public class SalesTaxAssignmentContext : DbContext
    {
        public SalesTaxAssignmentContext(DbContextOptions<SalesTaxAssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<BasketItem> BasketItem { get; set; } = default!;
        public DbSet<Input> Input { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Input>().HasData(
                new Input { Id = 1, Name = "Input1" },
                new Input { Id = 2, Name = "Input2" },
                new Input { Id = 3, Name = "Input3" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Book", Price = 12.49m, Category = Category.Book, IsImported = false },
                new Product { Id = 2, Name = "Music CD", Price = 14.99m, Category = Category.Other, IsImported = false },
                new Product { Id = 3, Name = "Chocolate Bar", Price = 0.85m, Category = Category.Food, IsImported = false },
                new Product { Id = 4, Name = "Imported Box of Chocolates", Price = 10.00m, Category = Category.Food, IsImported = true },
                new Product { Id = 5, Name = "Imported Bottle of Perfume", Price = 47.50m, Category = Category.Other, IsImported = true },
                new Product { Id = 6, Name = "Imported Bottle of Perfume", Price = 27.99m, Category = Category.Other, IsImported = true },
                new Product { Id = 7, Name = "Bottle of Perfume", Price = 18.99m, Category = Category.Other, IsImported = false },
                new Product { Id = 8, Name = "Headache Pills", Price = 9.75m, Category = Category.Medical, IsImported = false },
                new Product { Id = 9, Name = "Imported Box of Chocolates", Price = 11.25m, Category = Category.Food, IsImported = true }
            );

            modelBuilder.Entity<BasketItem>().HasData(
                new BasketItem { Id = 1, ProductId = 1, Quantity = 1, BasketKey = "Input1", InputId = 1 },
                new BasketItem { Id = 2, ProductId = 2, Quantity = 1, BasketKey = "Input1", InputId = 1 },
                new BasketItem { Id = 3, ProductId = 3, Quantity = 1, BasketKey = "Input1", InputId = 1 },
                new BasketItem { Id = 4, ProductId = 4, Quantity = 1, BasketKey = "Input2", InputId = 2 },
                new BasketItem { Id = 5, ProductId = 5, Quantity = 1, BasketKey = "Input2", InputId = 2 },
                new BasketItem { Id = 6, ProductId = 6, Quantity = 1, BasketKey = "Input3", InputId = 3 },
                new BasketItem { Id = 7, ProductId = 7, Quantity = 1, BasketKey = "Input3", InputId = 3 },
                new BasketItem { Id = 8, ProductId = 8, Quantity = 1, BasketKey = "Input3", InputId = 3 },
                new BasketItem { Id = 9, ProductId = 9, Quantity = 1, BasketKey = "Input3", InputId = 3 }
            );

        }
    }
}



