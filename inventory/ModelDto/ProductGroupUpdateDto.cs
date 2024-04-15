namespace inventory.ModelDto
{
    public class ProductGroupUpdateDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentGroup { get; set; }
        public int? ProductGroupId { get; set; }
    }
}
