namespace SalesTaxAssignment.Models
{
    public class ReceiptViewModel
    {
        public string BasketKey { get; set; } = string.Empty;
        public List<ReceiptItemViewModel> Items { get; set; } = new();
    }

    public class ReceiptItemViewModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsImported { get; set; }
        public Category Category { get; set; }
    }
}
