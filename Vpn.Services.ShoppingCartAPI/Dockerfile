FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Vpn.Services.ShoppingCartAPI/Vpn.Services.ShoppingCartAPI.csproj", "Vpn.Services.ShoppingCartAPI/"]
RUN dotnet restore "Vpn.Services.ShoppingCartAPI/Vpn.Services.ShoppingCartAPI.csproj"
COPY . .
WORKDIR "/src/Vpn.Services.ShoppingCartAPI"
RUN dotnet build "Vpn.Services.ShoppingCartAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vpn.Services.ShoppingCartAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vpn.Services.ShoppingCartAPI.dll"]
