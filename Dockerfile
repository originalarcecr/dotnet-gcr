FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
RUN dotnet publish -o /app/published-app --configuration Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/published-app /app

# Ensure the SQLite database file is writable
RUN chmod 644 ./geography.db

ENTRYPOINT ["dotnet", "TodoApi.dll"]