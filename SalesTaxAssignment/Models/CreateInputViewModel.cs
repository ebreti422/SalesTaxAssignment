namespace SalesTaxAssignment.Models
{
    public class CreateInputViewModel
    {
        public string BasketKey { get; set; } = string.Empty;
        public List<ProductInputViewModel> Products { get; set; } = new();
    }

    public class ProductInputViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }

}