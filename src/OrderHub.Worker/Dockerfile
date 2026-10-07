FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/OrderHub.Core/OrderHub.Core.csproj src/OrderHub.Core/
COPY src/OrderHub.Worker/OrderHub.Worker.csproj src/OrderHub.Worker/
RUN dotnet restore src/OrderHub.Worker/OrderHub.Worker.csproj

COPY src/ src/
RUN dotnet publish src/OrderHub.Worker/OrderHub.Worker.csproj -c Release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "OrderHub.Worker.dll"]
