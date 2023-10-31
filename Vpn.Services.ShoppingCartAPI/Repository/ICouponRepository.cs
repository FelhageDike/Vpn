using Vpn.Services.ShoppingCartAPI.Models.Dto;

namespace Vpn.Services.ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
