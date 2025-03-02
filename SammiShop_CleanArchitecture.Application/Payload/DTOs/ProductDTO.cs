namespace SammiShop_CleanArchitecture.Application.Payload.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public string UrlImg { get; set; }
        public decimal Price { get; set; }
        public Guid ProductTypeId { get; set; }
        public int Quantity { get; set; }
        public Guid TrademarkId { get; set; }
    }
}
