namespace Vpn.Services.ShoppingCartAPI.Models.Dto
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
