# ----------------------
# FASE BASE: runtime
# ----------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

USER root

# Instalar librerías nativas necesarias + libc
RUN apt-get update \
    && apt-get install -y \
        libleptonica-dev \
        libtesseract-dev \
        libc6-dev \
    && rm -rf /var/lib/apt/lists/*

# Crear directorio x64 y copiar Leptonica al mismo que las DLLs
RUN mkdir -p /app/x64 \
 && cp /usr/lib/x86_64-linux-gnu/liblept.so.5 /app/x64/libleptonica-1.82.0.so \
 && chmod 755 /app/x64/libleptonica-1.82.0.so

# Copiar libdl y crear symlink para .NET
RUN cp /usr/lib/x86_64-linux-gnu/libdl.so.2 /app/x64/ \
 && cd /app/x64 \
 && ln -s libdl.so.2 libdl.so \
 && chmod 755 libdl.so*

# Configurar LD_LIBRARY_PATH para que .NET encuentre librerías del sistema y nativas
ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu:/app/x64:$LD_LIBRARY_PATH

USER $APP_UID

# ----------------------
# FASE BUILD
# ----------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
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
