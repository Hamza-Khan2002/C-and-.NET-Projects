# FinanceProject

Lightweight ASP.NET Core Web API for managing stocks, portfolios and comments with JWT authentication and integration with FinancialModelingPrep (FMP) for stock data.

## Key Features
- User registration & login (Identity + JWT)
- Stock CRUD (protected endpoints)
- User portfolios (add/remove stocks)
- Comment system for stocks
- External stock data from FinancialModelingPrep (FMP)
- EF Core migrations and SQL Server support

## Tech
- .NET 10
- ASP.NET Core Web API
- Entity Framework Core (SQL Server)
- ASP.NET Core Identity
- JWT Bearer authentication
- AutoMapper

## Prerequisites
- .NET 10 SDK
- SQL Server instance (or change provider)
- (Optional) dotnet-ef tools for applying migrations: `dotnet tool install --global dotnet-ef`

## Quick start
1. Clone the repo and open the solution or change directory to the project:

   cd "FinanceProject"

2. Copy the example configuration and update values:

   - Copy `appsettings.Example.json` -> `appsettings.json` (or update the existing `appsettings.json`).
   - Set `ConnectionStrings:DefaultConnection` to your SQL Server connection string.
   - Set `JWT:SigningKey` (a strong secret) and `JWT:Issuer` / `JWT:Audience` as needed.
   - Set `FMP_API_KEY` environment variable or place the key in configuration.

3. Restore and run migrations:

   dotnet restore
   dotnet ef database update

4. Run the API:

   dotnet run --project FinanceProject.csproj

5. Open Swagger (in development) at `https://localhost:<port>/swagger` to explore endpoints and test requests.

## Important endpoints
- POST api/account/register  — register a new user
- POST api/account/login     — login and receive JWT
- GET/POST/PUT/DELETE api/stock  — stock CRUD (requires Authorization)
- GET/POST api/portfolio    — view/add portfolio items (requires Authorization)
- GET/POST/PUT/DELETE api/comment — comments for stocks

## Notes
- Some endpoints require an Authorization: Bearer <token> header. Use the login endpoint to obtain a token.
- The project uses `FMPService` to fetch stock profile data from FinancialModelingPrep (requires an API key in `FMP_API_KEY`).

## Project Link
- http://managefinance.runasp.net/swagger/index.html

