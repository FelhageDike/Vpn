using Vpn.Services.ShoppingCartAPI.Models.Dto;

namespace Vpn.Services.ShoppingCartAPI.Repository;

public interface ICartRepository
{
    Task<CartDto> GetCartByUSerId(Guid userId);
    Task<CartDto> CreateUpdateCart(CartDto cartDto);
    Task<bool> RemoveFromCart(Guid cartDetailsId);
    Task<bool> ClearCart(Guid userId);
}