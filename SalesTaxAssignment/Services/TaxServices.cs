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
            decimal taxRate = 0;

            bool isExempt = product.Category == Category.Book
                         || product.Category == Category.Food
                         || product.Category == Category.Medical;

            if (!isExempt)
                taxRate += 0.10m;

            if (product.IsImported)
                taxRate += 0.05m;

            var rawTax = product.Price * taxRate;
            var roundedTax = Math.Ceiling(rawTax * 20) / 20m;

            return roundedTax;
        }

        public decimal PriceWithTax(Product product)
        {
            return product.Price + CalculateTax(product);
        }
    }

}
