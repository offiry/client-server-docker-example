FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app

EXPOSE 9191

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Client/ClientService/ClientService.csproj", "ClientService/"]
COPY ["Client/Application/Application.csproj", "Application/"]
COPY ["Client/Application.Persistance/Application.Persistance.csproj", "Application.Persistance/"]
COPY ["Client/Bootstrapper/Bootstrapper.csproj", "Bootstrapper/"]
COPY ["Client/Domain/Domain.csproj", "Domain/"]
COPY ["Client/Infrastructure/Infrastructure.csproj", "Infrastructure/"]

RUN dotnet restore "ClientService/ClientService.csproj"
COPY . .
WORKDIR "/src/Client/ClientService"
RUN dotnet build "ClientService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientService.dll"]