#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Enscool.Bootstrapper/Enscool.Bootstrapper.csproj", "src/Enscool.Bootstrapper/"]
COPY ["src/Modules/Management/Modules.Management.Api/Modules.Management.Api.csproj", "src/Modules/Management/Modules.Management.Api/"]
COPY ["src/Modules/Management/Modules.Management.Infrastructure/Modules.Management.Infrastructure.csproj", "src/Modules/Management/Modules.Management.Infrastructure/"]
COPY ["src/Core/Core.Infrastructure/Core.Infrastructure.csproj", "src/Core/Core.Infrastructure/"]
COPY ["src/Core/Core.Application/Core.Application.csproj", "src/Core/Core.Application/"]
COPY ["src/Core/Core.Domain/Core.Domain.csproj", "src/Core/Core.Domain/"]
COPY ["src/Common/Common.Utilities/Common.Utilities.csproj", "src/Common/Common.Utilities/"]
COPY ["src/Modules/Management/Modules.Management.Application/Modules.Management.Application.csproj", "src/Modules/Management/Modules.Management.Application/"]
COPY ["src/Modules/Management/Modules.Management.Domain/Modules.Management.Domain.csproj", "src/Modules/Management/Modules.Management.Domain/"]
RUN dotnet restore "./src/Enscool.Bootstrapper/./Enscool.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/src/Enscool.Bootstrapper"
RUN dotnet build "./Enscool.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enscool.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enscool.Bootstrapper.dll", "--urls", "http://0.0.0.0:8080"]