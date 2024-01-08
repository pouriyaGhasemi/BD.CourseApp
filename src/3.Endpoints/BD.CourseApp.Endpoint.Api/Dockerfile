#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/3.Endpoints/BD.CourseApp.Endpoint.Api/BD.CourseApp.Endpoint.Api.csproj", "src/3.Endpoints/BD.CourseApp.Endpoint.Api/"]
COPY ["src/1.Core/BD.CourseApp.Core.ApplicationService/BD.CourseApp.Core.ApplicationService.csproj", "src/1.Core/BD.CourseApp.Core.ApplicationService/"]
COPY ["src/1.Core/BD.CourseApp.Core.DomainService/BD.CourseApp.Core.DomainService.csproj", "src/1.Core/BD.CourseApp.Core.DomainService/"]
COPY ["src/1.Core/BD.CourseApp.Core.Domain/BD.CourseApp.Core.Domain.csproj", "src/1.Core/BD.CourseApp.Core.Domain/"]
COPY ["src/2.Infrastructures/Data/BD.CourseApp.Infrastructures.Data.SqlServer/BD.CourseApp.Infrastructures.Data.SqlServer.csproj", "src/2.Infrastructures/Data/BD.CourseApp.Infrastructures.Data.SqlServer/"]
RUN dotnet restore "./src/3.Endpoints/BD.CourseApp.Endpoint.Api/./BD.CourseApp.Endpoint.Api.csproj"
COPY . .
WORKDIR "/src/src/3.Endpoints/BD.CourseApp.Endpoint.Api"
RUN dotnet build "./BD.CourseApp.Endpoint.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BD.CourseApp.Endpoint.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BD.CourseApp.Endpoint.Api.dll"]