using Microsoft.AspNetCore.Mvc;
using Vpn.Services.ShoppingCartAPI.Models.Dto;
using Vpn.Services.ShoppingCartAPI.Repository;

namespace Vpn.Services.ShoppingCartAPI.Controllers;
[ApiController]
[Route("api/cart")]
public class CartController : Controller
{
    private readonly ICartRepository _cartRepository;
    protected ResponseDto _responseDto;

    public CartController(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
        _responseDto = new ResponseDto();
    }
    [HttpGet("GetCart/{userId:guid}")]
    public async Task<object> GetCart(Guid userId)
    {
        try
        {
            var cart = await _cartRepository.GetCartByUSerId(userId);
            _responseDto.Result = cart;
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _responseDto;
    }
    
    
    [HttpPost("AddCart")]
    public async Task<object> AddCart(CartDto cartDto)
    {
        try
        {
            var cartDt = await _cartRepository.CreateUpdateCart(cartDto);
            _responseDto.Result = cartDt;
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string>() { e.ToString() };
            Console.WriteLine(_responseDto);
        }
        return _responseDto;
    }
    
    
    [HttpPost("UpdateCart")]
    public async Task<object> UpdateCart(CartDto cartDto)
    {
        try
        {
            var cartDt = await _cartRepository.CreateUpdateCart(cartDto);
            _responseDto.Result = cartDt;
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _responseDto;
    }    
    
    [HttpPost("RemoveCart")]
    public async Task<object> RemoveCart([FromBody]Guid cartId)
    {
        try
        {
            var isSuccess = await _cartRepository.RemoveFromCart(cartId);
            _responseDto.Result = isSuccess;
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _responseDto;
    }
    
    [HttpPost("ClearCart")]
    public async Task<object> ClearCart([FromBody]Guid cartId)
    {
        try
        {
            var isSuccess = await _cartRepository.ClearCart(cartId);
            _responseDto.Result = isSuccess;
        }
        catch (Exception e)
        {
            _responseDto.IsSuccess = false;
            _responseDto.ErrorMessages = new List<string>() { e.ToString() };
        }
        return _responseDto;
    }
    
    
    
}