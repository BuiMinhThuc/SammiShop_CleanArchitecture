namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Bill : BaseEntity<Guid>
    {
        public Guid UserId { get; set; }
        public DateTime? CreateTime { get; set; } = DateTime.Now;
        public decimal? TotalPrice { get; set; }
        public virtual ICollection<HistoryPay>? HistoryPays { get; set; }
        public virtual User? User { get; set; }
    }
}
