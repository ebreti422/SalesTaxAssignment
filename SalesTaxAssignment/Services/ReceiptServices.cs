using Microsoft.EntityFrameworkCore;
using SalesTaxAssignment.Data;
using SalesTaxAssignment.Models;

namespace SalesTaxAssignment.Services
{
    public class ReceiptLine
    {
        public required string Description { get; set; }
        public decimal PriceWithTax { get; set; }
    }

    public class Receipt
    {
        public List<ReceiptLine> Lines { get; set; } = new();
        public decimal SalesTaxes { get; set; }
        public decimal Total { get; set; }
    }

    public interface IReceiptService
    {
        Receipt GenerateReceipt(string basketKey);
        Receipt GenerateReceipt(List<BasketItem> items);
    }

    public class ReceiptService : IReceiptService
    {
        private readonly SalesTaxAssignmentContext _context;
        private readonly ITaxService _taxService;

        public ReceiptService(SalesTaxAssignmentContext context, ITaxService taxService)
        {
            _context = context;
            _taxService = taxService;
        }

        public Receipt GenerateReceipt(string basketKey)
        {
            var items = _context.BasketItem
                .Include(b => b.Product)
                .Where(b => b.BasketKey == basketKey)
                .ToList();

            var receipt = new Receipt();

            foreach (var item in items)
            {
                var tax = _taxService.CalculateTax(item.Product);
                var unitPriceWithTax = item.Product.Price + tax;
                var lineTotal = unitPriceWithTax * item.Quantity;

                receipt.Lines.Add(new ReceiptLine
                {
                    Description = $"{item.Quantity} {item.Product.Name}",
                    PriceWithTax = Math.Round(lineTotal, 2)
                });

                receipt.SalesTaxes += tax * item.Quantity;
                receipt.Total += lineTotal;
            }

            receipt.SalesTaxes = Math.Round(receipt.SalesTaxes, 2);
            receipt.Total = Math.Round(receipt.Total, 2);

            return receipt;
        }

        public Receipt GenerateReceipt(List<BasketItem> items)
        {
            var receipt = new Receipt();

            foreach (var item in items)
            {
                var tax = _taxService.CalculateTax(item.Product);
                var unitPriceWithTax = item.Product.Price + tax;
                var lineTotal = unitPriceWithTax * item.Quantity;

                receipt.Lines.Add(new ReceiptLine
                {
                    Description = $"{item.Quantity} {item.Product.Name}",
                    PriceWithTax = Math.Round(lineTotal, 2)
                });

                receipt.SalesTaxes += tax * item.Quantity;
                receipt.Total += lineTotal;
            }

            receipt.SalesTaxes = Math.Round(receipt.SalesTaxes, 2);
            receipt.Total = Math.Round(receipt.Total, 2);

            return receipt;
        }
    }
}
