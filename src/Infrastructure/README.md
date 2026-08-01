# Setup

cd into src/Infrastructure and then install the following:

## EntityFrameworkCore for PostgreSQL
```bash
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 10.0.3
```

!! install the following in src/Api:

To be able to run migrations, also required:
```bash
dotnet add package Microsoft.EntityFrameworkCore.Design --version 10.0.10
```

## CLI for migrations
```bash
dotnet tool install --global dotnet-ef
```

### Run first migration
Run from the root of the repository, not from within this Infrastructure location:

```bash
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/Api
```

after migration run to update the database:
```bash
dotnet ef database update --project src/Infrastructure --startup-project src/Api
```
