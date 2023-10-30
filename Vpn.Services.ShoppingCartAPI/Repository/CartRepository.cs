using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Vpn.Services.ShoppingCartAPI.DbContexts;
using Vpn.Services.ShoppingCartAPI.Models;
using Vpn.Services.ShoppingCartAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;
namespace Vpn.Services.ShoppingCartAPI.Repository;

public class CartRepository : ICartRepository
{
    
    private readonly ApplicationDbContext _context;
    private IMapper _mapper;

    public CartRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    

    public async Task<CartDto> CreateUpdateCart(CartDto cartDto)
    {

            Cart cart = _mapper.Map<Cart>(cartDto);

            //check if product exists in database, if not create it!
            var prodInDb = await _context.Products
                .FirstOrDefaultAsync(u => u.ProductId == cartDto.CartDetails.FirstOrDefault()!
                .ProductId);
            if (prodInDb == null)
            {
                var product = new Product()
                {
                    ProductId = cart.CartDetails.FirstOrDefault().ProductId,
                    ImageUrl = cart.CartDetails.FirstOrDefault().Product.ImageUrl,
                    Name = cart.CartDetails.FirstOrDefault().Product.Name,
                    Price = cart.CartDetails.FirstOrDefault().Product.Price,
                };
                _context.Products.Add(product);
                
            }


            //check if header is null
            var cartHeaderFromDb = await _context.CartHeaders.AsNoTracking().FirstOrDefaultAsync(u => u.UserId == cart.CartHeader.UserId);

            if (cartHeaderFromDb == null)
            {
                //create header and details
                _context.CartHeaders.Add(cart.CartHeader);
                await _context.SaveChangesAsync();
                var cartDetails = cart.CartDetails.FirstOrDefault();
                cartDetails.CartHeaderId = cart.CartHeader.CartHeaderId;
                cartDetails.Product = null;
                _context.CartDetails.Add(cartDetails);
                
            }
            else
            {
                //if header is not null
                //check if details has same product
                 _context.CartDetails.AsNoTracking();
                var cartDetailsFromDb = await _context.CartDetails.AsNoTracking().FirstOrDefaultAsync(
                    u => u.ProductId == cart.CartDetails.FirstOrDefault().ProductId &&
                    u.CartHeaderId == cartHeaderFromDb.CartHeaderId);

                if (cartDetailsFromDb == null)
                {
                    //create details
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartHeaderFromDb.CartHeaderId;
                    cart.CartDetails.FirstOrDefault().Product = null;
                    _context.CartDetails.Add(cart.CartDetails.FirstOrDefault());
                   
                }
                else
                {
                    //update the count / cart details
                    cart.CartDetails.FirstOrDefault().Product = null;
                    cart.CartDetails.FirstOrDefault().Count += cartDetailsFromDb.Count;
                    cart.CartDetails.FirstOrDefault().CartDetailsId = cartDetailsFromDb.CartDetailsId;
                    cart.CartDetails.FirstOrDefault().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                    _context.CartDetails.Update(cart.CartDetails.FirstOrDefault());
                    
                }
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<CartDto>(cart);

    }
    
 

    public async Task<bool> ClearCart(Guid userId)
    {
        var cartHeaderFromDb = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId);
        if (cartHeaderFromDb is null)
        {
            _context.CartDetails
                .RemoveRange(_context.CartDetails.Where(x=> x.CartHeaderId == cartHeaderFromDb.CartHeaderId));
            _context.CartHeaders.Remove(cartHeaderFromDb);
            await _context.SaveChangesAsync();
            return true;
        }

        return false;
    }
    
    
    public async Task<CartDto> GetCartByUSerId(Guid userId)
    {
        var cart = new Cart()
        {
            CartHeader = await _context.CartHeaders.FirstOrDefaultAsync(x => x.UserId == userId)
        };
        cart.CartDetails = _context.CartDetails
            .Where(x => x.CartHeaderId == cart.CartHeader.CartHeaderId)
            .Include(x=> x.Product);

        return _mapper.Map<CartDto>(cart);
    }

    public async Task<bool> RemoveFromCart(Guid cartDetailsId)
    {
        try
        {
            CartDetails cartDetails = await _context.CartDetails
                .FirstOrDefaultAsync(u => u.CartDetailsId == cartDetailsId);

            int totalCountOfCartItems = _context.CartDetails
                .Where(u => u.CartHeaderId == cartDetails.CartHeaderId).Count();

            _context.CartDetails.Remove(cartDetails);
            if (totalCountOfCartItems == 1)
            {
                var cartHeaderToRemove = await _context.CartHeaders
                    .FirstOrDefaultAsync(u => u.CartHeaderId == cartDetails.CartHeaderId);

                _context.CartHeaders.Remove(cartHeaderToRemove);
            }
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception e)
        {
            return false;
        }
    }
}