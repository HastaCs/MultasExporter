# Imagen base de .NET Runtime (para ejecutar la aplicación)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Exponer el puerto 80
EXPOSE 8080

# Copiar los archivos publicados desde tu máquina local (de la carpeta ./publish)
COPY /bin/Release/net8.0/publish .

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "API.dll"]

