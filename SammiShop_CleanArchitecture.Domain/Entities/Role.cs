namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Role : BaseEntity<int>
    {
        public string KeyRole { get; set; }
        public virtual ICollection<User>? Users { get; set; }
    }
}
