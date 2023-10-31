using Vpn.Services.ShoppingCartAPI.Models.Dto;

namespace Vpn.Services.ShoppingCartAPI.Repository
{
    public interface ICartRepository
    {
        Task<CartDto> GetCartByUserId(Guid userId);
        Task<CartDto> CreateUpdateCart(CartDto cartDto);
        Task<bool> RemoveFromCart(int cartDetailsId);
        Task<bool> ApplyCoupon(Guid userId, string couponCode);
        Task<bool> RemoveCoupon(Guid userId);
        Task<bool> ClearCart(Guid userId);
    }
}
