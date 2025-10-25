namespace SalesTaxAssignment.Models
{
    public class Input
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    }
}
