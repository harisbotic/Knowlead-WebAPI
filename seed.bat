call .\environment.bat
cd src
cd Knowlead.Tools
dotnet build
dotnet run seed SeedData
pause