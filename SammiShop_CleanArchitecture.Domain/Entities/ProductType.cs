namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class ProductType : BaseEntity<Guid>
    {
        public string TypeName { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
