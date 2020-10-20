#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["API-GEO/API-GEO.csproj", "API-GEO/"]
COPY ["API-GEO.Manager/API-GEO.Manager.csproj", "API-GEO.Manager/"]
COPY ["API-GEO.MQ/API-GEO.MQ.csproj", "API-GEO.MQ/"]
COPY ["API-GEO.Models/API-GEO.Models.csproj", "API-GEO.Models/"]
COPY ["API-GEO.Data/API-GEO.Data.csproj", "API-GEO.Data/"]
RUN dotnet restore "API-GEO/API-GEO.csproj"
COPY . .
WORKDIR "/src/API-GEO"
RUN dotnet build "API-GEO.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "API-GEO.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API-GEO.dll"]