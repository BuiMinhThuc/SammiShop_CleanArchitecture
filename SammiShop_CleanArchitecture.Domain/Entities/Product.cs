namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Product : BaseEntity<Guid>
    {
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
        public Guid ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public Guid TrademarkId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public virtual Trademark? Trademark { get; set; }
        public virtual ICollection<Card>? Cards { get; set; }
        public virtual ICollection<HistorryPay>? HistorryPays { get; set; }
    }
}
