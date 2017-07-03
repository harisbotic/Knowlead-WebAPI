call .\environment.bat
cd src
cd Knowlead.DAL
dotnet build
dotnet ef database update --startup-project=../Knowlead.WebApi
pause