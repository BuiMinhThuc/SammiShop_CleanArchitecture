namespace SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest
{
    public class UpdateCardRequest
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
