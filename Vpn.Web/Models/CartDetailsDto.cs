namespace Vpn.Web.Models;

public class CartDetailsDto
{
    public Guid CartHeaderId { get; set; }
    public CartHeaderDto CartHeader { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; }
    public int Count { get; set; }
}