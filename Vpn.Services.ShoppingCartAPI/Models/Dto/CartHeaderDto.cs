namespace Vpn.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public Guid CartHeaderId { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
