using Vpn.Services.ProductAPI.Models;
using Vpn.Services.ProductAPI.Models.Dto;

namespace Vpn.Services.ProductAPI.Repository;

public interface IProductRepository
{
    Task<IEnumerable<ProductDto>> GetProducts();
    Task<ProductDto> GetProductById(Guid productId);
    Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
    Task<bool> DeleteProduct(Guid productId);
}