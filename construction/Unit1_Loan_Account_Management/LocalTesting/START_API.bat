@echo off
echo ================================================================================
echo MANSERV Local Test API - Starting Server
echo ================================================================================
echo.
echo Attempting to start API server on different ports...
echo.

cd /d "%~dp0API"

echo Trying port 5000...
dotnet run --urls "http://localhost:5000"

if errorlevel 1 (
    echo.
    echo Port 5000 failed, trying port 5001...
    dotnet run --urls "http://localhost:5001"
)

if errorlevel 1 (
    echo.
    echo Port 5001 failed, trying port 5002...
    dotnet run --urls "http://localhost:5002"
)

if errorlevel 1 (
    echo.
    echo All ports failed. Please check for port conflicts.
    echo Try running: netstat -ano | findstr :500
    pause
)