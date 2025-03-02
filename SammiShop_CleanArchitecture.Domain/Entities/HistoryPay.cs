namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class HistoryPay : BaseEntity<Guid>
    {
        public Guid BillId { get; set; }

        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Bill? Bill { get; set; }

    }
}
