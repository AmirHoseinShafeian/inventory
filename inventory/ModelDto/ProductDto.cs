﻿namespace inventory.ModelDto
{
    public class ProductDto
    {
        public string Name { get; set; }
        public int ProductGroupId { get; set; }
        public int Price { get; set; }
        public DateTime? Exp { get; set; }
    }
}
