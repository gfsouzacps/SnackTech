# Use uma imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Define o diretório de trabalho no contêiner
WORKDIR /src

# Copie os arquivos .csproj para o contêiner
COPY core/SnackTech.Domain/SnackTech.Domain.csproj core/SnackTech.Domain/
COPY core/SnackTech.Application/SnackTech.Application.csproj core/SnackTech.Application/
COPY infra.persistence/SnackTech.Driver.DataBase/SnackTech.Driver.DataBase.csproj infra.persistence/SnackTech.Driver.DataBase/
COPY infra.web-api/SnackTech.Driver.API/SnackTech.Driver.API.csproj infra.web-api/SnackTech.Driver.API/

# Restaure as dependências
RUN dotnet restore infra.web-api/SnackTech.Driver.API/SnackTech.Driver.API.csproj

# Copie todos os arquivos restantes
COPY . ./

# Compile o projeto
RUN dotnet build infra.web-api/SnackTech.Driver.API/SnackTech.Driver.API.csproj -c Release -o /app/build

# Use a imagem de runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho no contêiner
WORKDIR /src

# Copie a build da etapa anterior
COPY --from=build-env /app/build .

# Exponha a porta da API
EXPOSE 8080
EXPOSE 8081

# Defina o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "SnackTech.Driver.API.dll"]
