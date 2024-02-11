# Migrations
## Create
```console
dotnet ef migrations add Initial --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContext --output-dir Migrations\ModelDb
```

## Update
```console
dotnet ef database update --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContext
```
