namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Card : BaseEntity<Guid>
    {
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
        public virtual Product? Product { get; set; }
        public virtual User? User { get; set; }
    }
}
