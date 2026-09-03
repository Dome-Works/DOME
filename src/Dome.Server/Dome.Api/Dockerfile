# Build context: repository root
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:e1ffd2a92ae84c1291bc1b6887501f8af98e6331e7af6d4c8d37168c5e87a64c AS build
WORKDIR /build

COPY Directory.Build.props Directory.Packages.props global.json ./

COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/HomelabDocs.Socket.Contracts.csproj src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/
COPY src/HomelabDocs.Server/HomelabDocs.Shared/HomelabDocs.Shared.csproj src/HomelabDocs.Server/HomelabDocs.Shared/
COPY src/HomelabDocs.Server/HomelabDocs.Domain/HomelabDocs.Domain.csproj src/HomelabDocs.Server/HomelabDocs.Domain/
COPY src/HomelabDocs.Server/HomelabDocs.Business/HomelabDocs.Business.csproj src/HomelabDocs.Server/HomelabDocs.Business/
COPY src/HomelabDocs.Server/HomelabDocs.Api/HomelabDocs.Api.csproj src/HomelabDocs.Server/HomelabDocs.Api/

RUN dotnet restore src/HomelabDocs.Server/HomelabDocs.Api/HomelabDocs.Api.csproj

COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/ src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/
COPY src/HomelabDocs.Server/HomelabDocs.Shared/ src/HomelabDocs.Server/HomelabDocs.Shared/
COPY src/HomelabDocs.Server/HomelabDocs.Domain/ src/HomelabDocs.Server/HomelabDocs.Domain/
COPY src/HomelabDocs.Server/HomelabDocs.Business/ src/HomelabDocs.Server/HomelabDocs.Business/
COPY src/HomelabDocs.Server/HomelabDocs.Api/ src/HomelabDocs.Server/HomelabDocs.Api/

RUN dotnet publish src/HomelabDocs.Server/HomelabDocs.Api/HomelabDocs.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:a4556ed033fa96f984bb7a8d348851cb2d36b1281dd2420070045f664fbb5f94 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HomelabDocs.Api.dll"]
