# Build context: repository root
FROM mcr.microsoft.com/dotnet/sdk:10.0@sha256:35d40304542c8689331f8cab17c65926cdf48fe711e289321d71924b230a7d29 AS build
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

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:2d584d8147faddb0d678c5748d47953e5b8e18621ed4fb7049a91381d9d7746f AS final
COPY --from=docker:27-cli@sha256:851f91d241214e7c6db86513b270d58776379aacc5eb9c4a87e5b47115e3065c /usr/local/bin/docker /usr/local/bin/docker
COPY --from=docker:27-cli@sha256:851f91d241214e7c6db86513b270d58776379aacc5eb9c4a87e5b47115e3065c /usr/local/libexec/docker/cli-plugins/docker-compose /usr/local/libexec/docker/cli-plugins/docker-compose
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Dome.Socket.Api.dll"]
