#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app

#to use swagger in docker 
ENV ASPNETCORE_ENVIRONMENT=Development

#ports that container listens at runtime 
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
#if using docker-compose use "./Schools.WebApi/SchoolWebApi.csproj"
COPY ["./Schools.WebApi/SchoolWebApi.csproj", "."]
RUN dotnet restore "./SchoolWebApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./Schools.WebApi/SchoolWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Schools.WebApi/SchoolWebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SchoolWebApi.dll"]