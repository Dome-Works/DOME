# Build context: repository root
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:e1ffd2a92ae84c1291bc1b6887501f8af98e6331e7af6d4c8d37168c5e87a64c AS build
WORKDIR /build

COPY Directory.Build.props Directory.Packages.props global.json ./
COPY src/Dome.Socket/Dome.Socket.Contracts/Dome.Socket.Contracts.csproj src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj src/Dome.Socket/Dome.Socket.Api/

RUN dotnet restore src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj

COPY src/Dome.Socket/Dome.Socket.Contracts/ src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Socket/Dome.Socket.Api/ src/Dome.Socket/Dome.Socket.Api/

RUN dotnet publish src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:a4556ed033fa96f984bb7a8d348851cb2d36b1281dd2420070045f664fbb5f94 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Dome.Socket.Api.dll"]
