FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
RUN apt-get update && apt-get install -y sqlite3 libsqlite3-dev
WORKDIR /app
COPY --from=build /app/published-app /app

ENTRYPOINT ["dotnet", "TodoApi.dll"]