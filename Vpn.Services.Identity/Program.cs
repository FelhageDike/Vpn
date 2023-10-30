using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vpn.Services.Identity;
using Vpn.Services.Identity.DbContexts;
using Vpn.Services.Identity.Initializer;
using Vpn.Services.Identity.Models;
using Vpn.Services.Identity.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var configuration = builder.Configuration;



builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;
        options.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryIdentityResources(SD.IdentityResources)
    .AddInMemoryApiScopes(SD.ApiScopes)
    .AddInMemoryClients(SD.Clients)
    .AddAspNetIdentity<ApplicationUser>()
    .AddDeveloperSigningCredential();


builder.Services.AddScoped<IProfileService, ProfileService>();




var app = builder.Build();

    

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

