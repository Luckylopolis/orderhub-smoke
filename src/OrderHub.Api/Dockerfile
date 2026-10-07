FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/OrderHub.Core/OrderHub.Core.csproj src/OrderHub.Core/
COPY src/OrderHub.Api/OrderHub.Api.csproj src/OrderHub.Api/
RUN dotnet restore src/OrderHub.Api/OrderHub.Api.csproj

COPY src/ src/
RUN dotnet publish src/OrderHub.Api/OrderHub.Api.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OrderHub.Api.dll"]
