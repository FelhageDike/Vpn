using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vpn.Services.ProductAPI.Models;
using Vpn.Services.ProductAPI.Models.Dto;
using Vpn.Services.ProductAPI.Repository;

namespace Vpn.Services.ProductAPI.Controllers;
[Route("api/products")]
public class ProductApiController : ControllerBase
{
    protected ResponseDto _response;
    private IProductRepository _productRepository;


    public ProductApiController( IProductRepository productRepository)
    {
        _response = new ResponseDto();
        _productRepository = productRepository;
    }
    
    [HttpGet]
    public async Task<ResponseDto> GetAllProducts()
    {
        try
        {
            var productsDto = await _productRepository.GetProducts();
            _response.Result = productsDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpGet]
    [Route("{productId:guid}")]
    public async Task<ResponseDto> GetById(Guid productId)
    {
        try
        {
            var productsDto = await _productRepository.GetProductById(productId);
            _response.Result = productsDto;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
    {
        try
        {
            var model = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = model;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpPut]
    [Authorize]
    public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
    {
        try
        {
            var model = await _productRepository.CreateUpdateProduct(productDto);
            _response.Result = model;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{productId:guid}")]
    public async Task<ResponseDto> Delete(Guid productId)
    {
        try
        {
            var isSuccess = await _productRepository.DeleteProduct(productId);
            _response.Result = isSuccess;
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new List<string>() { ex.ToString() };
        }

        return _response;
    }
    
}