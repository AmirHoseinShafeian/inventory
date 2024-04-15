using System.ComponentModel.DataAnnotations.Schema;

namespace inventory.Entities
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductGroup ParentGroup { get; set; }
        public int? ParentGroupId { get; set; }
        public int ProductGroupCode { get; set; }

        public List<Product> Products { get; set; }

    }
}
