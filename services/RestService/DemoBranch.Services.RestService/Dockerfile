#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["services/RestService/DemoBranch.Services.RestService/DemoBranch.Services.RestService.csproj", "services/RestService/DemoBranch.Services.RestService/"]
RUN dotnet restore "services/RestService/DemoBranch.Services.RestService/DemoBranch.Services.RestService.csproj"
COPY . .
WORKDIR "/src/services/RestService/DemoBranch.Services.RestService"
RUN dotnet build "DemoBranch.Services.RestService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DemoBranch.Services.RestService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DemoBranch.Services.RestService.dll"]