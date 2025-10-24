namespace SalesTaxAssignment.Models
{
    public enum Category { Book, Food, Medical, Other }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public bool IsImported { get; set; }
    }

}
