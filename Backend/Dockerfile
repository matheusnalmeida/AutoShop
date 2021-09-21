#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV PORT=$PORT

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AutoShop.Api/AutoShop.Api.csproj", "AutoShop.Api/"]
COPY ["DDDWebAPI.Infrastruture.CrossCutting.IOC/DDDWebAPI.Infrastruture.CrossCutting.IOC.csproj", "DDDWebAPI.Infrastruture.CrossCutting.IOC/"]
COPY ["AutoShop.Domain/AutoShop.Domain.csproj", "AutoShop.Domain/"]
COPY ["AutoShop.Shared/AutoShop.Shared.csproj", "AutoShop.Shared/"]
COPY ["AutoShop.Domain.Service/AutoShop.Domain.Service.csproj", "AutoShop.Domain.Service/"]
COPY ["AutoShop.Application/AutoShop.Application.csproj", "AutoShop.Application/"]
COPY ["AutoShop.Infra/AutoShop.Infra.csproj", "AutoShop.Infra/"]
RUN dotnet restore "AutoShop.Api/AutoShop.Api.csproj"
COPY . .
WORKDIR "/src/AutoShop.Api"
RUN dotnet build "AutoShop.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AutoShop.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AutoShop.Api.dll"]