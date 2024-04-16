namespace inventory.ModelDto
{
    public class ProductReportByNameDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductGroupName { get; set; }
        public int Count { get; set; }
    }

    public class ProductReportDto: ProductReportByNameDto
    {
        public int Price { get; set; }
        
    }
}
