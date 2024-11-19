
# Etapa 1: Construção do Frontend Angular
FROM node:22 AS build-angular
WORKDIR /app

# Copiar arquivos essenciais do Angular
COPY ClientApp/client/package*.json ./
RUN npm install
RUN npm install -g @angular/cli

# Copiar os demais arquivos e compilar
COPY ClientApp/client/. .
RUN ng build --configuration=production

# Etapa 2: Construção do Backend ASP.NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-bff
WORKDIR /src

# Copiar o arquivo de projeto da biblioteca GrpcContracts
COPY GrpcContracts/GrpcContracts.csproj ./GrpcContracts/

# Copiar o arquivo de projeto do CatalogBff
COPY CatalogBff/CatalogBff.csproj ./CatalogBff/

# Restaurar dependências
RUN dotnet restore ./CatalogBff/CatalogBff.csproj

# Copiar os códigos-fonte
COPY GrpcContracts/ ./GrpcContracts/
COPY CatalogBff/ ./CatalogBff/

# Publicar o projeto CatalogBff
WORKDIR /src/CatalogBff
RUN dotnet publish -c Release -o /app/publish

# Etapa 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copiar o resultado do backend publicado
COPY --from=build-bff /app/publish .

# Copiar o frontend Angular compilado para o wwwroot do backend
COPY --from=build-angular /app/dist/client ./wwwroot

# Configurações finais
EXPOSE 80
ENTRYPOINT ["dotnet", "CatalogBff.dll"]
