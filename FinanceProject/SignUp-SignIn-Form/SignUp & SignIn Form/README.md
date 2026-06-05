# SignUp & SignIn Form

Simple ASP.NET Core project for user registration and authentication.

## Summary
Project contains Models, DTOs, Repository, and Middleware for JWT handling. Targets .NET 10.

## Prerequisites
- .NET 10 SDK
- Visual Studio 2022/2026 or VS Code

## Quick setup
1. Open the solution in Visual Studio or from terminal:
   - Visual Studio: Open the solution file and press F5.
   - CLI: dotnet restore
           dotnet build
           dotnet run --project "SignUp & SignIn Form"

2. Configure settings:
   - Add a connection string in `appsettings.json` under `ConnectionStrings`.
   - Add JWT secret under `JwtConfig:Secret` (strong random string).

3. Database:
   - If using EF Core, add a migrations project and run `dotnet ef database update` after configuring the DbContext connection string.

## Notes / TODO
- Register ApplicationDbContext and repositories in `Program.cs` (AddDbContext, AddScoped).
- Implement password hashing (BCrypt/Argon2) before storing passwords.
- Add controllers and input validation.
- Remove `obj/` and `bin/` from source control (.gitignore added).

## How to view a file (Middleware/JwtMiddleware.cs)
- In Visual Studio: Open Solution Explorer -> expand project -> Middleware -> double-click JwtMiddleware.cs.
- From PowerShell (project root):
  - Open in Notepad: notepad "Middleware\JwtMiddleware.cs"
  - Print to console: Get-Content "Middleware\JwtMiddleware.cs" | more

## Contact / Contribution
Create issues or pull requests for fixes or features.

