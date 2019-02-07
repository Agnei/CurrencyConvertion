FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk-alpine AS build
WORKDIR /src

COPY src/Adapters/ConversorMoeda.WebApi/ConversorMoeda.WebApi.csproj src/Adapters/ConversorMoeda.WebApi/
COPY src/Infrastructure/Infrastructure.Core/Infrastructure.Core.csproj src/Infrastructure/Infrastructure.Core/
COPY src/Application/ConversorMoeda.Application/ConversorMoeda.Application.csproj src/Application/ConversorMoeda.Application/
COPY src/Adapters/ConversorMoeda.CurrencyService/ConversorMoeda.CurrencyService.csproj src/Adapters/ConversorMoeda.CurrencyService/

RUN dotnet restore src/Adapters/ConversorMoeda.WebApi/ConversorMoeda.WebApi.csproj

COPY . .
WORKDIR /src/src/Adapters/ConversorMoeda.WebApi

RUN dotnet build ConversorMoeda.WebApi.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish ConversorMoeda.WebApi.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

ENTRYPOINT ["dotnet", "ConversorMoeda.WebApi.dll"]
