using Vpn.Web.Models;

namespace Vpn.Web.Services.IServices;

public interface ICartService
{
    Task<T> GetCartByUserIdAsync<T>(Guid userId, string token = null);
    Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null);
    Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null);
    Task<T> RemoveFromCartAsync<T>(Guid carID, string token = null);
}