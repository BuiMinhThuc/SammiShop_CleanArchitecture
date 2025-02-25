namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class RefreshToken : BaseEntity<Guid>
    {
        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime Exprited { get; set; }
        public virtual User? User { get; set; }

    }
}
