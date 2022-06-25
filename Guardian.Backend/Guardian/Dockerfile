#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Guardian/Guardian.csproj", "Guardian/"]
COPY ["Guardian.Infrastructure/Guardian.Infrastructure.csproj", "Guardian.Infrastructure/"]
COPY ["Guardian.Service/Guardian.Service.csproj", "Guardian.Service/"]
COPY ["Guardian.Persistence/Guardian.Persistence.csproj", "Guardian.Persistence/"]
COPY ["Guardian.Domain/Guardian.Domain.csproj", "Guardian.Domain/"]
RUN dotnet restore "Guardian/Guardian.csproj"
RUN dotnet dev-certs https
COPY . .
WORKDIR "/src/Guardian"
RUN dotnet build "Guardian.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Guardian.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Guardian.dll"]