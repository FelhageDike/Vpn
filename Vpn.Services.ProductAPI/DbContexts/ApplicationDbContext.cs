using Microsoft.EntityFrameworkCore;
using Vpn.Services.ProductAPI.Models;
using Vpn.Services.ProductAPI.Models.Dto;

namespace Vpn.Services.ProductAPI.DbContexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
        
    }


    public DbSet<Product> Products { get; set; }
    
    
    
}