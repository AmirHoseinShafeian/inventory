namespace inventory.ModelDto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ProductGroupId { get; set; }
        public int? Price { get; set; }
        public DateTime? Exp { get; set; }
    }
}
