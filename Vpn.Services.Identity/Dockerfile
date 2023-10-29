FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Vpn.Services.Identity/Vpn.Services.Identity.csproj", "Vpn.Services.Identity/"]
RUN dotnet restore "Vpn.Services.Identity/Vpn.Services.Identity.csproj"
COPY . .
WORKDIR "/src/Vpn.Services.Identity"
RUN dotnet build "Vpn.Services.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vpn.Services.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vpn.Services.Identity.dll"]
