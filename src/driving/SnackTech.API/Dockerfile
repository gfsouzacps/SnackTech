#Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS base
##instalando icu-libs (InvariantGlobalization)
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
##Configurando usuario e porta
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

#Construção com SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

#Copiando csproj e restore
COPY ["SnackTech.API.csproj", "SnackTech.API/"]
RUN dotnet restore "SnackTech.API/SnackTech.API.csproj"

#Cópia dos arquivos e build
WORKDIR "/src/SnackTech.API"
COPY . .
RUN dotnet build "SnackTech.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

#Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SnackTech.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#Entrypoint com imagem do runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SnackTech.API.dll"]