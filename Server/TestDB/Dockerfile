# Base image with SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy the entire project folder
COPY . ./

# Restore dependencies
RUN dotnet restore

# Publish the app
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/runtime:8.0
WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "TestDB.dll"]
