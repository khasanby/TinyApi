# TinyApi

A minimal .NET 8 Web API project.

## Build and Run

```bash
dotnet restore
dotnet build
dotnet run
```

## Endpoints

- `GET /health` - Returns health status with UTC timestamp
- `GET /items` - Returns all items
- `GET /items/{id}` - Returns a specific item by ID
- `POST /items` - Creates a new item

## Swagger

Swagger UI is available at `/swagger` in both Development and Production environments.

