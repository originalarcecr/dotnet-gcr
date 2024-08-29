# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TodoApi.csproj", "./"]
RUN dotnet restore "TodoApi.csproj"
COPY . .
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Install SQLite
RUN apt-get update && apt-get install -y sqlite3 libsqlite3-dev

# Create a directory for the SQLite database
RUN mkdir -p /app/data
ENV DatabasePath=/app/data/todo.db

# Expose the port your app runs on
EXPOSE 8080

ENTRYPOINT ["dotnet", "TodoApi.dll"]