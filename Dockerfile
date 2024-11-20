FROM node:22 AS build-angular
WORKDIR /app
COPY CatalogBff/ClientApp/client/package*.json ./
RUN npm install
RUN npm install -g @angular/cli
COPY CatalogBff/ClientApp/client/. .
RUN ng build --configuration production


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-bff
WORKDIR /src
COPY CatalogBff/ ./CatalogBff/
COPY GrpcContracts/ ./GrpcContracts/
RUN dotnet restore CatalogBff/CatalogBff/CatalogBff.csproj
RUN dotnet publish CatalogBff/CatalogBff/CatalogBff.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build-bff /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "CatalogBff.dll"]
