# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
ARG APP_UID=1000
USER $APP_UID

# Build SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore
COPY A2CGateway.csproj ./
RUN dotnet restore "./A2CGateway.csproj"

# Copy all source code
COPY . ./
WORKDIR "/src"

RUN dotnet build "A2CGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "A2CGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "A2CGateway.dll"]
