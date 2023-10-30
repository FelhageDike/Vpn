﻿using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vpn.Services.Identity.DbContexts;
using Vpn.Services.Identity.Models;

namespace Vpn.Services.Identity.Initializer;

public class DbInitializer : IDbInitializer
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;


    public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public void Initialize()
    {
        _context.Database.Migrate();
        if (_roleManager.FindByNameAsync(SD.Admin).Result == null)
        {
            _roleManager.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
        }
        else
        {
            return;
        }

        var adminUser = new ApplicationUser()
        {
            UserName = "adminUser",
            Email = "adminUser@mail.ru",
            EmailConfirmed = true,
            PhoneNumber = "89898789098",
            FirstName ="vlad",
            LastName = "adminUser"
        };
        _userManager.CreateAsync(adminUser, "FelhageDike1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(adminUser, SD.Admin).GetAwaiter().GetResult();

        var temp1 = _userManager.AddClaimsAsync(adminUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, adminUser.FirstName + " " + adminUser.LastName),
            new Claim(JwtClaimTypes.GivenName, adminUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, adminUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Admin),
        }).Result;
        
        var customerUser = new ApplicationUser()
        {
            UserName = "customerUser",
            Email = "customerUser@mail.ru",
            EmailConfirmed = true,
            PhoneNumber = "89898789098",
            FirstName ="vlad",
            LastName = "customerUser"
        };
        _userManager.CreateAsync(customerUser, "FelhageDike1!").GetAwaiter().GetResult();
        _userManager.AddToRoleAsync(customerUser, SD.Customer).GetAwaiter().GetResult();

        var temp2 = _userManager.AddClaimsAsync(customerUser, new Claim[]
        {
            new Claim(JwtClaimTypes.Name, customerUser.FirstName + " " + customerUser.LastName),
            new Claim(JwtClaimTypes.GivenName, customerUser.FirstName),
            new Claim(JwtClaimTypes.FamilyName, customerUser.LastName),
            new Claim(JwtClaimTypes.Role, SD.Customer),
        }).Result;
        
    }
}