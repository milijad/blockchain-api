# IC Markets – Blockchain Snapshot Web API (.NET)  

This project is a .NET Web API built with Clean Architecture (Domain / Application / Infrastructure / API) to fetch blockchain snapshot data from BlockCypher and store the full JSON responses in a PostgreSQL database with a CreatedAt timestamp. It also exposes history endpoints for each blockchain.  

## Tech stack / patterns
- .NET 8
- Clean Architecture + SOLID
- CQRS with MediatR
- Repository + Unit of Work
- FluentResults (result pattern)
- FluentValidation
- Swagger (OpenAPI)
- Health checks (liveness + readiness)
- PostgreSQL (Docker)
- Tests: Unit, Integration, Functional (Testcontainers)

Supported BlockCypher sources:
- ETH mainnet: https://api.blockcypher.com/v1/eth/main
- DASH mainnet: https://api.blockcypher.com/v1/dash/main
- BTC mainnet: https://api.blockcypher.com/v1/btc/main
- BTC testnet (test3): https://api.blockcypher.com/v1/btc/test3
- LTC mainnet: https://api.blockcypher.com/v1/ltc/main

---

## Running locally (without Docker)  

Prerequisites:
- .NET SDK 8+
- PostgreSQL running locally

Create appsettings.Development.json (not committed):
```json
{  
  "ConnectionStrings": {  
    "Postgres": "Host=localhost;Port=5432;Database=blockchain;Username=icmarkets;Password=icmarkets"  
  }  
}  
```
Run migrations:  
dotnet tool install --global dotnet-ef  
dotnet ef database update --project src/Blockchain.Infrastructure --startup-project src/Blockchain.Api  

Run API:

dotnet run --project src/Blockchain.Api

Swagger: http://localhost:5263/swagger

---

## Running with Docker

Create .env file (not committed):  

POSTGRES_CONNECTION=Host=postgres;Port=5432;Database=blockchain;Username=icmarkets;Password=icmarkets  

Run:

docker compose up -d --build

Wait for migration to finish (check readiness probe for Healthy status). 

Swagger: http://localhost:5000/swagger  
Health:  
http://localhost:5000/health/live – liveness probe (process up)  
http://localhost:5000/health/ready – readiness probe (PostgreSQL + BlockCypher reachable)  

---

## API Endpoints

Fetch snapshot:  
POST /api/blockchain/eth/fetch  
POST /api/blockchain/dash/fetch  
POST /api/blockchain/btc/main/fetch  
POST /api/blockchain/btc/test3/fetch  
POST /api/blockchain/ltc/fetch  

History:  
GET /api/blockchain/{type}/history?limit=10  
type = EthMain | DashMain | BtcMain | BtcTest3 | LtcMain  

---

## Tests

Run all tests:
dotnet test

Run external integration tests:
dotnet test --filter Category=External

---

## Branching

development – active development  
main – stable branch  

---

## Notes

Credentials are injected via environment variables and are not stored in the repository.  
.env and appsettings.Development.json must be gitignored.
