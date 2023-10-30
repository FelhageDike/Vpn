using Vpn.Web.Models;

namespace Vpn.Web.Services.IServices;

public interface IProductServices : IBaseService
{
    Task<T> GetAllProductsAsync<T>(string token);
    Task<T> GetProductByIdAsync<T>(Guid id, string token);
    Task<T> CreateProductAsync<T>(ProductDto productDto, string token);
    Task<T> UpdateProductAsync<T>(ProductDto productDto,string token );
    Task<T> DeleteProductAsync<T>(Guid id, string token);
}