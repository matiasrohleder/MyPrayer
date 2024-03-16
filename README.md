# Migrations

## PostgreSQL
### Create
```sh
dotnet ef migrations add Initial --startup-project ..\WebApp\WebApp.csproj --context ModelsDbPostgreSQLContext --output-dir Migrations\ModelsDbPostgreSQL
```

### Update
```sh
dotnet ef database update --startup-project ..\WebApp\WebApp.csproj --context ModelsDbPostgreSQLContext
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