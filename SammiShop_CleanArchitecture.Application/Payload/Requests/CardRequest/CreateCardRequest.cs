namespace SammiShop_CleanArchitecture.Application.Payload.Requests.CardRequest
{
    public class CreateCardRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
