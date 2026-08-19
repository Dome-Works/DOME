# Build context: src/HomelabDocs.Server
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:e1fc6e423f543119c406d24e2e687d67c569f18f04a37a8b0005d80ad0dcee80 AS build
WORKDIR /src

COPY Directory.Build.props Directory.Packages.props global.json ./
COPY HomelabDocs.Shared/HomelabDocs.Shared.csproj HomelabDocs.Shared/
COPY HomelabDocs.Business/HomelabDocs.Business.csproj HomelabDocs.Business/
COPY HomelabDocs.Domain/HomelabDocs.Domain.csproj HomelabDocs.Domain/
COPY HomelabDocs.Api/HomelabDocs.Api.csproj HomelabDocs.Api/

RUN dotnet restore HomelabDocs.Api/HomelabDocs.Api.csproj

COPY HomelabDocs.Shared/ HomelabDocs.Shared/
COPY HomelabDocs.Business/ HomelabDocs.Business/
COPY HomelabDocs.Domain/ HomelabDocs.Domain/
COPY HomelabDocs.Api/ HomelabDocs.Api/

RUN dotnet publish HomelabDocs.Api/HomelabDocs.Api.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:207cc51496778557731c81ff670333d8ade4a4fec22768fd1be8e78474a84ecf AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "HomelabDocs.Api.dll"]
