namespace SammiShop_CleanArchitecture.Domain.Entities
{
    public class Feedback : BaseEntity<Guid>
    {
        public Guid UserId { set; get; }
        public string Content { set; get; }
        public int Star { set; get; }
        public virtual User? User { set; get; }


    }
}
