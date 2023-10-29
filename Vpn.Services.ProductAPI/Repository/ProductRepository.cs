using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vpn.Services.ProductAPI.DbContexts;
using Vpn.Services.ProductAPI.Models;
using Vpn.Services.ProductAPI.Models.Dto;

namespace Vpn.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _context;
    private IMapper _mapper;

    public ProductRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

   
    
    
    
    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        var productList = await _context.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(productList);
    }

    public async Task<ProductDto> GetProductById(Guid productId)
    {
        var product = await _context.Products.Where(x=> x.Id == productId).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        var product = _mapper.Map<ProductDto, Product>(productDto);
        var isExists = await _context.Products.AnyAsync(x => x.Id == product.Id);
        if (isExists)
        {
            _context.Products.Update(product);
        }
        else
        {
            _context.Products.Add(product);
        }

        await _context.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x=> x.Id == productId);
            if (product == null)
            {
                return false;
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
       
    }
}