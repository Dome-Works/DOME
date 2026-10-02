# Build context: repository root
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:35d40304542c8689331f8cab17c65926cdf48fe711e289321d71924b230a7d29 AS build
WORKDIR /build

COPY Directory.Build.props Directory.Packages.props global.json ./

COPY src/Dome.Socket/Dome.Socket.Contracts/Dome.Socket.Contracts.csproj src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Server/Dome.Shared/Dome.Shared.csproj src/Dome.Server/Dome.Shared/
COPY src/Dome.Server/Dome.Domain/Dome.Domain.csproj src/Dome.Server/Dome.Domain/
COPY src/Dome.Server/Dome.Business/Dome.Business.csproj src/Dome.Server/Dome.Business/
COPY src/Dome.Server/Dome.Api/Dome.Api.csproj src/Dome.Server/Dome.Api/

RUN dotnet restore src/Dome.Server/Dome.Api/Dome.Api.csproj

COPY src/Dome.Socket/Dome.Socket.Contracts/ src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Server/Dome.Shared/ src/Dome.Server/Dome.Shared/
COPY src/Dome.Server/Dome.Domain/ src/Dome.Server/Dome.Domain/
COPY src/Dome.Server/Dome.Business/ src/Dome.Server/Dome.Business/
COPY src/Dome.Server/Dome.Api/ src/Dome.Server/Dome.Api/

RUN dotnet publish src/Dome.Server/Dome.Api/Dome.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:2d584d8147faddb0d678c5748d47953e5b8e18621ed4fb7049a91381d9d7746f AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Dome.Api.dll"]
