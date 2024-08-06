# Use uma imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Define o diretório de trabalho no contêiner
WORKDIR /src

# Copie os arquivos .csproj para o contêiner
COPY driving/SnackTech.API/SnackTech.API.csproj driving/SnackTech.API/
COPY core/SnackTech.Application/SnackTech.Application.csproj core/
COPY driven/SnackTech.Adapter.DataBase/SnackTech.Adapter.DataBase.csproj driven/

# Restaure as dependências
RUN dotnet restore driving/SnackTech.API/SnackTech.API.csproj

# Copie todos os arquivos restantes
COPY . .

# Compile o projeto
RUN dotnet build driving/SnackTech.API/SnackTech.API.csproj -c Release -o /app/build

# Use a imagem de runtime do .NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho no contêiner
WORKDIR /app

# Copie a build da etapa anterior
COPY --from=build /app/build .

# Exponha a porta da API
EXPOSE 8080

# Defina o comando para rodar a aplicação
ENTRYPOINT ["dotnet", "SnackTech.API.dll"]
