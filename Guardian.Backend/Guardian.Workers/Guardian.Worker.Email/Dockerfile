#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Guardian.Workers/Guardian.Worker.Email/Guardian.Worker.Email.csproj", "Guardian.Workers/Guardian.Worker.Email/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure.Communication/Guardian.Infrastructure.Communication.csproj", "Guardian.Infrastructure/Guardian.Infrastructure.Communication/"]
COPY ["Guardian.Domain/Guardian.Domain.csproj", "Guardian.Domain/"]
COPY ["Guardian.Microservices/Guardian.Logging.Contract/Guardian.Logging.Contract.csproj", "Guardian.Microservices/Guardian.Logging.Contract/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure.Email/Guardian.Infrastructure.Email.csproj", "Guardian.Infrastructure/Guardian.Infrastructure.Email/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure.EventHub/Guardian.Infrastructure.EventHub.csproj", "Guardian.Infrastructure/Guardian.Infrastructure.EventHub/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure/Guardian.Infrastructure.csproj", "Guardian.Infrastructure/Guardian.Infrastructure/"]
COPY ["Guardian.Service/Guardian.Service.csproj", "Guardian.Service/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure.Database/Guardian.Infrastructure.Database.csproj", "Guardian.Infrastructure/Guardian.Infrastructure.Database/"]
RUN dotnet restore "Guardian.Workers/Guardian.Worker.Email/Guardian.Worker.Email.csproj"
COPY . .
WORKDIR "/src/Guardian.Workers/Guardian.Worker.Email"
RUN dotnet build "Guardian.Worker.Email.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guardian.Worker.Email.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guardian.Worker.Email.dll"]