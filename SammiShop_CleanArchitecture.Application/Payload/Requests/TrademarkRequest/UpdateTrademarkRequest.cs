namespace SammiShop_CleanArchitecture.Application.Payload.Requests.TrademarkRequest
{
    public class UpdateTrademarkRequest
    {
        public Guid Id { get; set; }
        public string TrademarkName { get; set; }
        public string Address { get; set; }
    }
}
