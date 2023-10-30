using System.ComponentModel.DataAnnotations.Schema;

namespace Vpn.Services.ShoppingCartAPI.Models
{
    public class CartDetails
    {
        public Guid CartDetailsId { get; set; }
        [ForeignKey("CartHeader")]
        public Guid CartHeaderId { get; set; }
       
        public virtual CartHeader CartHeader { get; set; }
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }
       
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
