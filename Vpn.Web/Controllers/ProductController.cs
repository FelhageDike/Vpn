using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Vpn.Web.Models;
using Vpn.Web.Services.IServices;

namespace Vpn.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductServices _productServices;

    public ProductController(IProductServices productServices)
    {
        _productServices = productServices;
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> list = new();
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productServices.GetAllProductsAsync<ResponseDto>(accessToken);
        if (response != null && response.IsSuccess)
        {
            list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
        }

        return View(list);
    }

    public async Task<IActionResult> ProductCreate()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductCreate(ProductDto model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        if (ModelState.IsValid)
        {
            var response = await _productServices.CreateProductAsync<ResponseDto>(model, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }
    public async Task<IActionResult> ProductEdit(Guid productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productServices.GetProductByIdAsync<ResponseDto>(productId, accessToken);
        if (response != null && response.IsSuccess)
        {
            ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ProductEdit(ProductDto model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        if (ModelState.IsValid)
        {
            var response = await _productServices.UpdateProductAsync<ResponseDto>(model, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ProductDelete(Guid productId)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var response = await _productServices.GetProductByIdAsync<ResponseDto>(productId, accessToken);
        if (response != null && response.IsSuccess)
        {
            ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
            return View(model);
        }
        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ProductDelete(ProductDto model)
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productServices.DeleteProductAsync<ResponseDto>(model.Id, accessToken);
            if (response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        

        return View(model);
    }
    
}