FROM node:22 AS build-angular
WORKDIR /app
COPY ClientApp/client/package*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY ClientApp/client/. .
RUN ng build --configuration=production

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-bff
WORKDIR /src
# Copiar o arquivo de projeto da biblioteca GrpcContracts
COPY ["../../GrpcContracts/GrpcContracts.csproj"] ./GrpcContracts/
COPY CatalogBff/ ./
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-bff /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "CatalogBff.dll"]
