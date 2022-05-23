# todo-api
Connection string is set for localhost, User = postgres, Password = 123. (in WebAPI/appsettings.json file)

1. Run `dotnet build .\WebAPI\WebAPI.csproj`
2. Run `dotnet ef database update --project .\Data\Data.csproj --startup-project .\WebAPI\WebAPI.csproj`
3. Run `dotnet run --project .\WebAPI\WebAPI.csproj`
4. SwaggerUI is at http://localhost:5000/swagger/index.html
