# Migrations
## Create
```sh
dotnet ef migrations add Initial --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContext --output-dir Migrations\ModelDb
```

## Update
```sh
dotnet ef database update --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContext
```
