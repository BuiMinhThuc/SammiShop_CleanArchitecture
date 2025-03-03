﻿namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Trademark : BaseEntity<Guid>
    {
        public string TrademarkName { get; set; }
        public string Address { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
