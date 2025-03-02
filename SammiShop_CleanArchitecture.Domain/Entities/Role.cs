namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Role : BaseEntity<Guid>
    {
        public string KeyRole { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
