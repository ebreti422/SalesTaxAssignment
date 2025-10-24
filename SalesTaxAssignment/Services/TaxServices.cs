using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Services
{
    public interface ITaxService
    {
        decimal CalculateTax(Product product);
        decimal PriceWithTax(Product product);
    }
    public class TaxService : ITaxService
    {
        public decimal CalculateTax(Product product)
        {
            decimal taxRate = 0m;

            // Basic tax: 10% if not exempt
            if (product.Category == Category.Other)
                taxRate += 0.10m;

            // Import duty: 5% if imported
            if (product.IsImported)
                taxRate += 0.05m;

            decimal rawTax = product.Price * taxRate;

            // Round up to nearest 0.05
            return Math.Ceiling(rawTax * 20) / 20m;
        }

        public decimal PriceWithTax(Product product)
        {
            return product.Price + CalculateTax(product);
        }
    }

}
