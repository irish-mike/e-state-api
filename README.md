# e-state API

The backend for **e-state**, an Irish property sales platform. It currently serves individual property listings and provides API and database health checks.

## Prerequisites

Make sure you have the following tools installed.

- .NET 10 SDK
- Docker with Docker Compose
- GNU Make
- A shell that provides `sh`, such as Git Bash on Windows

Make sure `make` and `sh` are on your PATH, then start Docker.

## Technologies

- **C# and ASP.NET Core Minimal APIs** on .NET 10
- **PostgreSQL 18** with Entity Framework Core and Npgsql
- **OpenAPI and Swagger UI** for interactive API documentation
- **Docker Compose and GNU Make** for local development
- **xUnit and GitHub Actions** for testing and CI

## Quick start

Configure the development connection string once from the repository root.

```sh
dotnet user-secrets set "ConnectionStrings:PostgreSql" "Host=localhost;Port=5432;Database=e_state_dev;Username=e_state;Password=e_state_dev_password" --project src/EState.Api
```

On a fresh local database, create the schema and load the sample data. **`make db-reset` deletes any existing data in the local development schema.**

```sh
make db-reset
make db-seed
```

Start PostgreSQL and the API.

```sh
make run
```

On subsequent starts, run `make run` again. Use `make test` to run the tests and `make help` to list the available commands.

### Explore the API

With the API running, open [Swagger UI](http://localhost:5165/swagger) to browse the endpoints, view request and response details, and send requests with **Try it out**. The [OpenAPI JSON](http://localhost:5165/openapi/v1.json) is also available. Both are enabled in the Development environment.

To try `GET /api/listings/{id}`, use the seeded apartment listing ID `40000000-0000-4000-8000-000000000001`.

## Formatting and linting

After `make restore`, run `make format` to fix C# formatting or `make check` to verify formatting and build with analyzers.
Rules live in [`.editorconfig`](.editorconfig).

## Further documentation

- [Architecture](docs/backend-architecture.md)
