# Complete Order Processing Solution

An interview-ready .NET 8 solution with shared domain models, pricing service, repository abstraction, REST API, typed Blazor API client, responsive Blazor WebAssembly UI, validation, and unit tests.

## Pricing assumptions

`Subtotal = sum(quantity × unit price)`; `Discount = subtotal × discount %`; `Tax = (subtotal − discount) × tax %`; `Grand total = subtotal − discount + tax + shipping`. Currency values use `decimal` and round to two places away from zero.

## Run

Open two terminals from this folder:

```bash
dotnet restore
dotnet run --project OrderProcessing.Api
dotnet run --project OrderProcessing.Client
```

Open `https://localhost:7102`. API Swagger is at `https://localhost:7101/swagger`. Run tests with `dotnet test OrderProcessing.Tests`.

## API

- `GET /api/orders`
- `GET /api/orders/{id}`
- `POST /api/orders`
- `PUT /api/orders/{id}`
- `DELETE /api/orders/{id}`

The create and update calls return an `OrderResponse` containing the saved order and server-calculated summary. The Blazor UI replaces its local model with that response. The Refresh button demonstrates an explicit re-fetch. Both approaches prevent stale front-end state; all async calls are awaited.

The repository is intentionally in-memory for a self-contained coding exercise. Replace `InMemoryOrderRepository` with an EF Core implementation without changing controllers or UI.
