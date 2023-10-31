using System.ComponentModel.DataAnnotations.Schema;

namespace Vpn.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        public int CartDetailsId { get; set; }
        public int CartHeaderId { get; set; }
        [ForeignKey("CartHeaderId")]
        public virtual CartHeader CartHeader { get; set; }
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
