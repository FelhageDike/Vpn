namespace Vpn.Web.Models;

public class CartHeaderDto
{
    public string UserId { get; set; }
    public string? CouponCode { get; set; }
    public double OrderTotal { get; set; }
}