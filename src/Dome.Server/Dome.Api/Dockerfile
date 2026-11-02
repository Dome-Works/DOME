# Build context: repository root
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0@sha256:de4fe90a98845d75910b9769662465c84de1a934665c431279d1382b2bddc02b AS build
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

ARG VERSION
# Telemetry environment label, baked into the assembly (not runtime configuration).
ARG DOME_ENVIRONMENT=Development
RUN if [ -n "$VERSION" ]; then \
      dotnet publish src/Dome.Server/Dome.Api/Dome.Api.csproj \
        -c Release \
        -o /app/publish \
        --no-restore \
        /p:UseAppHost=false \
        /p:Version="$VERSION" \
        /p:InformationalVersion="$VERSION" \
        /p:DomeEnvironment="$DOME_ENVIRONMENT"; \
    else \
      dotnet publish src/Dome.Server/Dome.Api/Dome.Api.csproj \
        -c Release \
        -o /app/publish \
        --no-restore \
        /p:UseAppHost=false \
        /p:DomeEnvironment="$DOME_ENVIRONMENT"; \
    fi

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:de0ee358cdd672f7017a003bed21e9ac9ed48a27c922a59b18175b6e82fa7fee AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Dome.Api.dll"]
