# Build context: src/HomelabDocs.Server
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Directory.Build.props Directory.Packages.props global.json ./
COPY HomelabDocs.Shared/HomelabDocs.Shared.csproj HomelabDocs.Shared/
COPY HomelabDocs.Business/HomelabDocs.Business.csproj HomelabDocs.Business/
COPY HomelabDocs.Api/HomelabDocs.Api.csproj HomelabDocs.Api/

RUN dotnet restore HomelabDocs.Api/HomelabDocs.Api.csproj

COPY HomelabDocs.Shared/ HomelabDocs.Shared/
COPY HomelabDocs.Business/ HomelabDocs.Business/
COPY HomelabDocs.Api/ HomelabDocs.Api/

RUN dotnet publish HomelabDocs.Api/HomelabDocs.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HomelabDocs.Api.dll"]
