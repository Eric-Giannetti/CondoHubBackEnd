# Estágio 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia todos os arquivos .csproj para restaurar as dependências
# Isso aproveita o cache do Docker. Se você não adicionar novos projetos, esta camada é reaproveitada.
COPY ["CondoHub.API/CondoHub.API.csproj", "CondoHub.API/"]
COPY ["CondoHub.DataBase/CondoHub.DataBase.csproj", "CondoHub.DataBase/"]
COPY ["CondoHub.Domain/CondoHub.Domain.csproj", "CondoHub.Domain/"]
COPY ["CondoHub.Infra/CondoHub.Infra.csproj", "CondoHub.Infra/"]
COPY ["CondoHub.Security/CondoHub.Security.csproj", "CondoHub.Security/"]
COPY ["CondoHub.Services/CondoHub.Services.csproj", "CondoHub.Services/"]

# Restaura as dependências do projeto principal (que puxa as demais)
RUN dotnet restore "CondoHub.API/CondoHub.API.csproj"

# Copia todo o código fonte
COPY . .

# Compila e publica apenas o projeto de entrada (API)
WORKDIR "/src/CondoHub.API"
RUN dotnet publish "CondoHub.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# curl é usado pelo healthcheck do docker-compose
RUN apt-get update && apt-get install -y --no-install-recommends curl && rm -rf /var/lib/apt/lists/*

COPY --from=build /app/publish .

# Ponto de entrada aponta para a DLL da API
ENTRYPOINT ["dotnet", "CondoHub.API.dll"]