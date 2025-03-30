# Imagen base para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos del proyecto y restaurar dependencias
COPY ["src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj", "src/SalesDatePrediction.Api/"]
COPY ["src/SalesDatePrediction.Application/SalesDatePrediction.Application.csproj", "src/SalesDatePrediction.Application/"]
COPY ["src/SalesDatePrediction.Domain/SalesDatePrediction.Domain.csproj", "src/SalesDatePrediction.Domain/"]
COPY ["src/SalesDatePrediction.Infrastructure/SalesDatePrediction.Infrastructure.csproj", "src/SalesDatePrediction.Infrastructure/"]

RUN dotnet restore "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj"
RUN dotnet restore "src/SalesDatePrediction.Application/SalesDatePrediction.Application.csproj"
RUN dotnet restore "src/SalesDatePrediction.Domain/SalesDatePrediction.Domain.csproj"
RUN dotnet restore "src/SalesDatePrediction.Infrastructure/SalesDatePrediction.Infrastructure.csproj"

# Copiar solo los archivos necesarios y compilar
COPY src/SalesDatePrediction.Api/. src/SalesDatePrediction.Api/
COPY src/SalesDatePrediction.Application/. src/SalesDatePrediction.Application/
COPY src/SalesDatePrediction.Domain/. src/SalesDatePrediction.Domain/
COPY src/SalesDatePrediction.Infrastructure/. src/SalesDatePrediction.Infrastructure/
RUN dotnet build "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj" -c Release -o /app/build

# Publicar
FROM build AS publish
RUN dotnet publish "src/SalesDatePrediction.Api/SalesDatePrediction.Api.csproj" -c Release -o /app/publish

# Imagen final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://0.0.0.0:80
ENTRYPOINT ["dotnet", "SalesDatePrediction.Api.dll"] 