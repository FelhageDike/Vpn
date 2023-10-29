using AutoMapper;
using Vpn.Services.ProductAPI.Models;
using Vpn.Services.ProductAPI.Models.Dto;

namespace Vpn.Services.ProductAPI;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            config.CreateMap<ProductDto, Product>();
            config.CreateMap<Product, ProductDto>();
        });
        return mappingConfig;
    }
}