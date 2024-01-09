#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/Cantaoria.Web/Cantaoria.Web.csproj", "Presentation/Cantaoria.Web/"]
COPY ["Core/Cantaoria.API.Application/Cantaoria.Application.csproj", "Core/Cantaoria.API.Application/"]
COPY ["Core/Cantaoria.Domain/Cantaoria.Domain.csproj", "Core/Cantaoria.Domain/"]
COPY ["Infrastructure/Cantaoria.Infrastructure/Cantaoria.Infrastructure.csproj", "Infrastructure/Cantaoria.Infrastructure/"]
COPY ["Infrastructure/Cantaoria.Persistence/Cantaoria.Persistence.csproj", "Infrastructure/Cantaoria.Persistence/"]
RUN dotnet restore "./Presentation/Cantaoria.Web/./Cantaoria.Web.csproj"
COPY . .
WORKDIR "/src/Presentation/Cantaoria.Web"
RUN dotnet build "./Cantaoria.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cantaoria.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cantaoria.Web.dll"]