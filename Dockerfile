# ----------------------
# FASE BASE: runtime
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER $APP_UID

# ----------------------
# FASE BUILD
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copiar los csproj
COPY ["API/MultasApi.csproj", "API/"]
COPY ["MultasExporter/MultasExporter.csproj", "MultasExporter/"]

# Restaurar dependencias
RUN dotnet restore "./API/MultasApi.csproj"

# Copiar el resto del código
COPY . .

WORKDIR "/src/API"

# Build del proyecto
RUN dotnet build "./MultasApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ----------------------
# FASE PUBLISH
# ----------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MultasApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ----------------------
# IMAGEN FINAL
# ----------------------
FROM base AS final
WORKDIR /app

# Copiar la app publicada
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MultasApi.dll"]
