# Build context: repository root
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:e1fc6e423f543119c406d24e2e687d67c569f18f04a37a8b0005d80ad0dcee80 AS build
WORKDIR /build

COPY Directory.Build.props Directory.Packages.props global.json ./
COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/HomelabDocs.Socket.Contracts.csproj src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/
COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Api/HomelabDocs.Socket.Api.csproj src/HomelabDocs.Socket/HomelabDocs.Socket.Api/

RUN dotnet restore src/HomelabDocs.Socket/HomelabDocs.Socket.Api/HomelabDocs.Socket.Api.csproj

COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/ src/HomelabDocs.Socket/HomelabDocs.Socket.Contracts/
COPY src/HomelabDocs.Socket/HomelabDocs.Socket.Api/ src/HomelabDocs.Socket/HomelabDocs.Socket.Api/

RUN dotnet publish src/HomelabDocs.Socket/HomelabDocs.Socket.Api/HomelabDocs.Socket.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:a4556ed033fa96f984bb7a8d348851cb2d36b1281dd2420070045f664fbb5f94 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HomelabDocs.Socket.Api.dll"]
