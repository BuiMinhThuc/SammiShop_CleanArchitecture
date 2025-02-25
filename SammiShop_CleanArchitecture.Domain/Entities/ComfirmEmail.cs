namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class ComfirmEmail : BaseEntity<Guid>
    {
        public string Otp { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public DateTime Exprited { get; set; } = DateTime.Now.AddMinutes(5);
        public bool Status { get; set; } = false;
        public virtual User? User { get; set; }

    }
}
