cd src
cd Knowlead.WebApi
dotnet ef migrations add hepek_%random%
dotnet ef database update
cd ..
cd ..