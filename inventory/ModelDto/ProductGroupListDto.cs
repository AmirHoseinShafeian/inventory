namespace inventory.ModelDto
{
    public class ProductGroupListDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentGroupId { get; set; }
        public string? ParentGroupName { get; set; }
        public int? Code { get; set; }
    }
}
