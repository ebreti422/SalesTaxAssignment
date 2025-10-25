using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Data
{
    public class SalesTaxAssignmentContext : DbContext
    {
        public SalesTaxAssignmentContext (DbContextOptions<SalesTaxAssignmentContext> options)
            : base(options)
        {
        }

        public DbSet<SalesTaxAssignment.Models.Product> Product { get; set; } = default!;
        public DbSet<SalesTaxAssignment.Models.BasketItem> BasketItem { get; set; } = default!;
        public DbSet<Input> Input { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Input>().HasData(
                new Input { Id = 1, Name = "Input1" },
                new Input { Id = 2, Name = "Input2" },
                new Input { Id = 3, Name = "Input3" }
            );

            modelBuilder.Entity<BasketItem>().HasData(
                new BasketItem { Id = 1, ProductId = 1, Quantity = 1, BasketKey = "Input1", InputId = 1 },
                new BasketItem { Id = 2, ProductId = 2, Quantity = 1, BasketKey = "Input1", InputId = 1 },
                new BasketItem { Id = 3, ProductId = 3, Quantity = 1, BasketKey = "Input2", InputId = 2 },
                new BasketItem { Id = 4, ProductId = 4, Quantity = 1, BasketKey = "Input3", InputId = 3 }
            );
        }
    }
}



