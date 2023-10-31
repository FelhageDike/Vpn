using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vpn.Web.Models;
using Vpn.Web.Services.IServices;

namespace Vpn.Web.Controllers;

public class CartController : Controller
{
    private readonly IProductServices _productServices;
    private readonly ICartService _cartService;
    

    public CartController(IProductServices productServices, ICartService cartService)
    {
        _productServices = productServices;
        _cartService = cartService;
    }

    public async Task<IActionResult> CartIndex()
    {
        
        return View(await LoadCartDtoBasedOnLoggedInUser());
    }

    private async Task<CartDto> LoadCartDtoBasedOnLoggedInUser()
    {
        try
        {
            var userId = Guid.Parse(User.Claims.FirstOrDefault(u => u.Type == "sub")?.Value);
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _cartService.GetCartByUserIdAsync<ResponseDto>(userId, accessToken);
            CartDto cartDto = new CartDto();
            if (response.IsSuccess)
            {
                cartDto = JsonConvert.DeserializeObject<CartDto>(Convert.ToString(response.Result));
            }

            if (cartDto.CartHeader != null)
            {
                foreach (var detail in cartDto.CartDetails)
                {
                    cartDto.CartHeader.OrderTotal += (detail.Product.Price * detail.Count);
                }
            
            }

            return cartDto;
        }
        catch
        {
            return new CartDto();
        }

        
    }
}