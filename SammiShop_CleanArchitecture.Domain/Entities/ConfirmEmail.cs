using SammiShop_CleanArchitecture.Domain.Constants;

namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class ConfirmEmail : BaseEntity<Guid>
    {
        public string Otp { get; set; }
        public string Email { get; set; }
        public Guid UserId { get; set; }
        public DateTime Expired { get; set; } = DateTime.Now.AddMinutes(ConstantDomain.TIME_EXPIRED_EMAIL);
        public bool Status { get; set; } = false;
        public virtual User? User { get; set; }

    }
}
