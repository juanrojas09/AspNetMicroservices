#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Se utiliza la imagen de ASP.NET Core 5.0 como la imagen base
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

# Se establece el directorio de trabajo en /app
WORKDIR /app

# Se exponen los puertos 80 y 443 para que el contenedor pueda recibir solicitudes web
EXPOSE 80
EXPOSE 443

# Se utiliza la imagen de SDK de .NET Core 5.0 para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Se establece el directorio de trabajo en /src
WORKDIR /src

# Se copia el archivo de proyecto de la aplicación al directorio de trabajo actual
COPY ["Services/Catalog/Catalog.API/Catalog.API.csproj", "Services/Catalog/Catalog.API/"]

# Se restauran las dependencias de la aplicación
RUN dotnet restore "Services/Catalog/Catalog.API/Catalog.API.csproj"

# Se copian todos los archivos al directorio de trabajo actual
COPY . .

# Se establece el directorio de trabajo en el directorio del proyecto de la aplicación
WORKDIR "/src/Services/Catalog/Catalog.API"

# Se compila la aplicación y se genera el resultado en /app/build
RUN dotnet build "Catalog.API.csproj" -c Release -o /app/build

# Se publica la aplicación y se genera el resultado en /app/publish
# Se establece la opción UseAppHost en false para que la aplicación se ejecute en modo "self-contained"
FROM build AS publish
RUN dotnet publish "Catalog.API.csproj" -c Release -o /app/publish 

# Se establece la imagen base final como la imagen de ASP.NET Core 5.0
FROM base AS final

# Se establece el directorio de trabajo en /app
WORKDIR /app

# Se copian los archivos publicados de la aplicación en el directorio actual
COPY --from=publish /app/publish .

# Se establece el punto de entrada de la aplicación
ENTRYPOINT ["dotnet", "Catalog.API.dll"]
