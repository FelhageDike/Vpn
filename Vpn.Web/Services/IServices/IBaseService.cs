using Vpn.Web.Models;

namespace Vpn.Web.Services.IServices;

public interface IBaseService : IDisposable
{
    ResponseDto responseModel { get; set; }
    Task<T> SendAsync<T>(ApiRequest apiRequest);
}