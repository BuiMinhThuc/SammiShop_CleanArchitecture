namespace SammiShop_CleanArchitecture.Application.Payload.DTOs
{
    public class CardDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }
        public int Quantity { get; set; }
    }
}
