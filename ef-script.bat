@echo off
set projectPath=/app/A2C.CRM.Api

echo Choose an EF Core operation:
echo 1) Add a migration
echo 2) Update the database
echo 3) List migrations
set /p choice=Enter your choice (1/2/3): 

if "%choice%"=="1" (
    set /p migrationName=Enter migration name: 
    docker compose run --rm ef bash -c "dotnet ef migrations add \"%migrationName%\" --project %projectPath%"
) else if "%choice%"=="2" (
    docker compose run --rm ef bash -c "dotnet ef database update --project %projectPath%"
) else if "%choice%"=="3" (
    docker compose run --rm ef bash -c "dotnet ef migrations list --project %projectPath%"
) else (
    echo Invalid choice. Please enter 1, 2, or 3.
)
