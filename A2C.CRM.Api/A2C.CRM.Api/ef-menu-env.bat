@echo off
setlocal enabledelayedexpansion

:menu
cls
echo ============================================
echo      Entity Framework Core Menu
echo ============================================
echo 1. List migrations
echo 2. Update database
echo 3. Add new migration
echo 4. Exit
echo ============================================
set /p choice=Enter your choice (1-4): 

if "%choice%"=="4" goto end

echo.
echo Select environment:
echo 1. Development (localhost)
echo 2. Docker (db)
set /p envChoice=Enter environment (1-2): 

if "%envChoice%"=="1" (
    set CONNECTION_STRING=Host=localhost;Port=5432;Database=a2c_crm;Username=postgres;Password=postgres
) else if "%envChoice%"=="2" (
    set CONNECTION_STRING=Host=db;Port=5432;Database=a2c_crm;Username=postgres;Password=postgres
) else (
    echo Invalid environment choice.
    pause
    goto menu
)

if "%choice%"=="1" (
    echo Listing migrations...
    dotnet ef migrations list --connection "!CONNECTION_STRING!"
    pause
    goto menu
)

if "%choice%"=="2" (
    echo Updating database...
    dotnet ef database update --connection "!CONNECTION_STRING!"
    pause
    goto menu
)

if "%choice%"=="3" (
    set /p MIGRATION_NAME=Enter migration name: 
    echo Adding migration '!MIGRATION_NAME!'...
    dotnet ef migrations add "!MIGRATION_NAME!"
    pause
    goto menu
)

echo Invalid choice.
pause
goto menu

:end
echo Exiting...
