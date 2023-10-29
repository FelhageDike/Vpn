using System.Net;
using Microsoft.IdentityModel.Logging;
using Vpn.Web;
using Vpn.Web.Services;
using Vpn.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpClient<IProductServices, ProductService>();
var configuration = builder.Configuration;
SD.ProductAPIBase = configuration["ServiceUrls:ProductAPI"];
builder.Services.AddScoped<IProductServices, ProductService>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    }).AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7249";
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "Vpn";
        options.ClientSecret = "secret";
        options.ResponseType = "code";
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";
        options.Scope.Add("vpn");
        options.SaveTokens = true;
    });

builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();