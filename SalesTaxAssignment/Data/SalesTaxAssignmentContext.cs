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
    }
}
