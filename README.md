# Migrations

## PostgreSQL
### Create
```sh
dotnet ef migrations add Initial --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContextPostgreSQL --output-dir Migrations\ModelsDbPostgreSQL
```

### Update
```sh
dotnet ef database update --startup-project ..\WebApp\WebApp.csproj --context ModelsDbContextPostgreSQL
```

## SQLServer
### Create
```sh
dotnet ef migrations add Initial --startup-project ..\WebApp\WebApp.csproj --context ModelsDbSQLContext  --output-dir Migrations\ModelsDbSQL
```

### Update
```sh
dotnet ef database update --startup-project ..\WebApp\WebApp.csproj --context ModelsDbSQLContext
```