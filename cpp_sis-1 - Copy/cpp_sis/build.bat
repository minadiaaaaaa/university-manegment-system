@echo off
echo Compiling SQLite...
gcc -c src/sqlite3.c -o sqlite3.o -Iinclude
if %ERRORLEVEL% NEQ 0 exit /b 1

echo Compiling Menus...
g++ -c src/AdminMenu.cpp -o AdminMenu.o -Iinclude
if %ERRORLEVEL% NEQ 0 exit /b 1

g++ -c src/StudentMenu.cpp -o StudentMenu.o -Iinclude
if %ERRORLEVEL% NEQ 0 exit /b 1

g++ -c src/ProfessorMenu.cpp -o ProfessorMenu.o -Iinclude
if %ERRORLEVEL% NEQ 0 exit /b 1

echo Compiling Main...
g++ -c main.cpp -o main.o -Iinclude
if %ERRORLEVEL% NEQ 0 exit /b 1

echo Linking SIS_CLI...
g++ -static main.o AdminMenu.o StudentMenu.o ProfessorMenu.o sqlite3.o -o SIS_CLI.exe
if %ERRORLEVEL% EQU 0 (
    echo Build Successful: SIS_CLI.exe created.
) else (
    echo Link Failed.
)
