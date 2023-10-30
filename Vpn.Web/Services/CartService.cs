using Vpn.Web.Models;
using Vpn.Web.Services.IServices;

namespace Vpn.Web.Services;

public class CartService : BaseService, ICartService
{
    private readonly IHttpClientFactory _clientFactory;
    public CartService(IHttpClientFactory httpClient, IHttpClientFactory clientFactory) : base(httpClient)
    {
        _clientFactory = clientFactory;
    } 
    
    public async Task<T> GetCartByUserIdAsync<T>(Guid userId, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.GET,
            Url = SD.ShopingCartAPIBase + "/api/cart/GetCart/" + userId,
            AccessToken = token
        });
    }

    public async Task<T> AddToCartAsync<T>(CartDto cartDto, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.ShopingCartAPIBase + "/api/cart/AddCart",
            AccessToken = token
        });
    }

    public async Task<T> UpdateCartAsync<T>(CartDto cartDto, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = cartDto,
            Url = SD.ShopingCartAPIBase + "/api/cart/UpdateCart",
            AccessToken = token
        });
    }

    public async Task<T> RemoveFromCartAsync<T>(Guid carID, string token = null)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = SD.ApiType.POST,
            Data = carID,
            Url = SD.ShopingCartAPIBase + "/api/cart/RemoveCart",
            AccessToken = token
        });
    }

    
}