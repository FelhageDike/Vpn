FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Vpn.Web/Vpn.Web.csproj", "Vpn.Web/"]
RUN dotnet restore "Vpn.Web/Vpn.Web.csproj"
COPY . .
WORKDIR "/src/Vpn.Web"
RUN dotnet build "Vpn.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Vpn.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Vpn.Web.dll"]
