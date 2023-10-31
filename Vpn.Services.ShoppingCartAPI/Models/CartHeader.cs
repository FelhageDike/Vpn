using System.ComponentModel.DataAnnotations;

namespace Vpn.Services.ShoppingCartAPI.Models
{
    public class CartHeader
    {
        [Key]
        public int CartHeaderId { get; set; }
        public Guid UserId { get; set; }
        public string? CouponCode { get; set; }
    }
}
