# Entity framework

## Migrations

Make sure that you have ef tool installed:

```bash
dotnet tool install --global dotnet-ef
```

To add new migrations use this command from solution level:

```bash
dotnet ef migrations add [NAME] -s src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj -o DAL/Migrations/
```

To remove migration use:

```bash
dotnet ef migrations remove -s src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj
```

To apply migrations use:

```bash
dotnet ef database update -s src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj -- [project arguments]
```

To drop the database:

```bash
dotnet ef database drop -s src/Terminal.Backend.Api/Terminal.Backend.Api.csproj -p src/Terminal.Backend.Infrastructure/Terminal.Backend.Infrastructure.csproj
```
