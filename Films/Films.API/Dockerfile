FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Films.API/Films.API.csproj", "Films.API/"]
RUN dotnet restore "Films.API/Films.API.csproj"
COPY . .
WORKDIR "/src/Films.API"
RUN dotnet build "Films.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Films.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Films.API.dll"]
