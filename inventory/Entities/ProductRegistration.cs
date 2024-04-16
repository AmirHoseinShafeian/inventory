namespace inventory.Entities
{
    public class ProductRegistration
    {
        public int Id { get; set; }
        public DateTime DataRegister { get; set; }
        public int ProductId { get; set; }
        public int ProductNumber { get; set; }
        public int PriceOfProduct { get; set; }
        public int TotalPrice { get; set; }
        public Product Product { get; set; }
    }
}
