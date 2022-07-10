#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Guardian.Microservices/Guardian.Logging.Api/Guardian.Logging.Api.csproj", "Guardian.Microservices/Guardian.Logging.Api/"]
COPY ["Guardian.Microservices/Guardian.Logging.Contract/Guardian.Logging.Contract.csproj", "Guardian.Microservices/Guardian.Logging.Contract/"]
RUN dotnet restore "Guardian.Microservices/Guardian.Logging.Api/Guardian.Logging.Api.csproj"
COPY . .
WORKDIR "/src/Guardian.Microservices/Guardian.Logging.Api"
RUN dotnet build "Guardian.Logging.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guardian.Logging.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guardian.Logging.Api.dll"]