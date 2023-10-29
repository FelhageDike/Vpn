using Vpn.Web.Models;

namespace Vpn.Web.Services.IServices;

public interface IProductServices : IBaseService
{
    Task<T> GetAllProductsAsync<T>();
    Task<T> GetProductByIdAsync<T>(Guid id);
    Task<T> CreateProductAsync<T>(ProductDto productDto);
    Task<T> UpdateProductAsync<T>(ProductDto productDto);
    Task<T> DeleteProductAsync<T>(Guid id);
}