FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Blockchain.Api/Blockchain.Api.csproj", "src/Blockchain.Api/"]
COPY ["src/Blockchain.Application/Blockchain.Application.csproj", "src/Blockchain.Application/"]
COPY ["src/Blockchain.Domain/Blockchain.Domain.csproj", "src/Blockchain.Domain/"]
COPY ["src/Blockchain.Infrastructure/Blockchain.Infrastructure.csproj", "src/Blockchain.Infrastructure/"]
RUN dotnet restore "src/Blockchain.Api/Blockchain.Api.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/Blockchain.Api/Blockchain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "src/Blockchain.Api/Blockchain.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Blockchain.Api.dll"]
