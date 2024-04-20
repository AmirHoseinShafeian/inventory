namespace inventory.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductGroupId { get; set; }
        public ProductGroup ProductGroup { get; set; }
        public int Price { get; set; }
        public string? Exp { get; set; }

        public List<ProductRegistration> ProductRegistrations { get; set; } = new List<ProductRegistration>();
        public List<ProductRemittance> ProductRemittances { get; set; } = new List<ProductRemittance>();
    }
}
