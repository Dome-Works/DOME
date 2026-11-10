# Build context: repository root
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0@sha256:de4fe90a98845d75910b9769662465c84de1a934665c431279d1382b2bddc02b AS build
WORKDIR /build

COPY Directory.Build.props Directory.Packages.props global.json ./
COPY src/Dome.Socket/Dome.Socket.Contracts/Dome.Socket.Contracts.csproj src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj src/Dome.Socket/Dome.Socket.Api/

RUN dotnet restore src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj

COPY src/Dome.Socket/Dome.Socket.Contracts/ src/Dome.Socket/Dome.Socket.Contracts/
COPY src/Dome.Socket/Dome.Socket.Api/ src/Dome.Socket/Dome.Socket.Api/

ARG VERSION
RUN if [ -n "$VERSION" ]; then \
      dotnet publish src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj \
        -c Release \
        -o /app/publish \
        --no-restore \
        /p:UseAppHost=false \
        /p:Version="$VERSION" \
        /p:InformationalVersion="$VERSION"; \
    else \
      dotnet publish src/Dome.Socket/Dome.Socket.Api/Dome.Socket.Api.csproj \
        -c Release \
        -o /app/publish \
        --no-restore \
        /p:UseAppHost=false; \
    fi

FROM mcr.microsoft.com/dotnet/aspnet:10.0@sha256:de0ee358cdd672f7017a003bed21e9ac9ed48a27c922a59b18175b6e82fa7fee AS final
COPY --from=docker:27-cli@sha256:851f91d241214e7c6db86513b270d58776379aacc5eb9c4a87e5b47115e3065c /usr/local/bin/docker /usr/local/bin/docker
COPY --from=docker:27-cli@sha256:851f91d241214e7c6db86513b270d58776379aacc5eb9c4a87e5b47115e3065c /usr/local/libexec/docker/cli-plugins/docker-compose /usr/local/libexec/docker/cli-plugins/docker-compose
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=
EXPOSE 8080

ENTRYPOINT ["dotnet", "Dome.Socket.Api.dll"]
