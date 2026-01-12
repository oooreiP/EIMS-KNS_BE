#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EIMS.API/EIMS.API.csproj", "EIMS.API/"]
COPY ["EIMS.Application/EIMS.Application.csproj", "EIMS.Application/"]
COPY ["EIMS.Domain/EIMS.Domain.csproj", "EIMS.Domain/"]
COPY ["EIMS.Infrastructure/EIMS.Infrastructure.csproj", "EIMS.Infrastructure/"]
RUN dotnet restore "./EIMS.API/EIMS.API.csproj"
COPY . .
WORKDIR "/src/EIMS.API"
RUN dotnet build "./EIMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EIMS.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EIMS.API.dll"]