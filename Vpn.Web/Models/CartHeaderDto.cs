namespace Vpn.Web.Models;

public class CartHeaderDto
{
    public int CartHeaderId { get; set; }
    public Guid UserId { get; set; }
    public string? CouponCode { get; set; }
    public double OrderTotal { get; set; }
}