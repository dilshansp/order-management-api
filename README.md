![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)
![EF Core](https://img.shields.io/badge/EF%20Core-8.0-green)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-✓-brightgreen)
![License](https://img.shields.io/badge/License-MIT-lightgrey)
![Build](https://img.shields.io/badge/Build-Passing-brightgreen)

🚀 Order Management API (.NET 8, Clean Architecture, EF Core, JWT)

A fully production-style Order Management REST API, built using .NET 8, Clean Architecture, CQRS, EF Core 8, SQL Server, JWT Authentication, Logging, and xUnit tests.

This project is designed as a recruiter-ready portfolio project to demonstrate real enterprise engineering practices.

---

📌 Features

🔷 Core Features
- Create, Read, Update, Delete (CRUD) Orders  
- Each Order includes multiple Order Items  
- Automatic Total Amount calculation  
- SQL Server database via EF Core 8  
- Repository pattern  
- Full separation of concerns


🔒 Authentication & Authorization
- JWT Authentication (access tokens)  
- Demo user accounts:
    - 'admin' → full access (including delete)
    - 'user' → restricted access (no delete)
- Role-based access control (RBAC)
- Secure endpoints using '[Authorize]' and '[Authorize(Roles = "...")]'


🧱 Architecture
- Clean Architecture  
- CQRS (Command/Query Responsibility Segregation)  
- MediatR for request handling  
- AutoMapper for DTO mapping  
- FluentValidation for business rules  
- Repository + Unit of Work pattern


🧪 Automated Testing
- xUnit Tests  
- Moq mocking  
- FluentAssertions  
- EF Core InMemory tests  
- Covers Handlers + Repository

---

📂 Project Structure (Clean Architecture)

```
OrderManagementSolution
|
src
├── OrderManagement.Domain
│ ├── Entities
│ └── ValueObjects
│
├── OrderManagement.Application
│ ├── Common
│ ├── Interfaces
│ ├── Models (DTOs)
│ ├── Features
│ │ ├── Orders
│ │ │ ├── Commands
│ │ │ └── Queries
│ └── MappingProfile.cs
│
├── OrderManagement.Infrastructure
│ ├── Persistence (DbContext + EF Configurations)
│ ├── Repositories
│ └── Auth (JWT Generation)
│
└── OrderManagement.API
├── Controllers
├── Program.cs
└── appsettings.json
```

---

🧱 Clean Architecture Diagram
```
              +--------------------------+
              |      Presentation        |
              |       (API Layer)        |
              +-------------+------------+
                            |
                            v
 +-----------------+ +------|-------+ +---------------------+
 | Domain | <----- | Application | <----- | Infrastructure |
 | (Entities) |   | (CQRS, DTOs)|  | (EF Core, Repos, JWT) |
 +-----------------+ +-------------+ +---------------------+
```

---

🔧 Built With

| Component    | Technology                          |
| ------------ | ----------------------------------- |
| Framework    | .NET 8                              |
| Architecture | Clean Architecture                  |
| Database     | SQL Server 2019 (Express supported) |
| ORM          | Entity Framework Core 8.0.22        |
| CQRS         | MediatR                             |
| Auth         | JWT (Bearer tokens)                 |
| Validation   | FluentValidation                    |
| Mapping      | AutoMapper                          |
| Testing      | xUnit, Moq, FluentAssertions        |
| API Docs     | Swagger / OpenAPI                   |

---

🔐 Authentication (JWT)

The API uses JWT Bearer Authentication with role-based authorization.

  🔑 Login Endpoint

    POST /api/auth/login

  Example Request
```
  json
  {
    "username": "admin",
    "password": "Admin123!"
  }

  Example Response
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR...",
    "role": "Admin"
  }
```
  Use the token in Swagger:
  ```
  Bearer <token_here>
```

---

🗂 API Endpoints


🟢 Public Endpoints

| Method | Route             | Description              |
| ------ | ----------------- | ------------------------ |
| POST   | `/api/auth/login` | Returns JWT access token |


🔐 Protected Endpoints (Authentication Required)

Orders
| Method | Route              | Role       | Description           |
| ------ | ------------------ | ---------- | --------------------- |
| GET    | `/api/orders`      | Any        | Get all orders        |
| GET    | `/api/orders/{id}` | Any        | Get order by ID       |
| POST   | `/api/orders`      | Admin/User | Create new order      |
| PUT    | `/api/orders/{id}` | Admin/User | Update existing order |
| DELETE | `/api/orders/{id}` | Admin only | Delete order          |


---

🛠 Getting Started (Local Setup)

1️⃣ Clone the repository
```
git clone https://github.com/dilshansp/OrderManagementAPI.git
cd OrderManagementAPI
```
2️⃣ Configure SQL Server

Update your connection string in:
OrderManagement.API/appsettings.json

Example:
```
"DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=OrderDb;Trusted_Connection=True;TrustServerCertificate=True;"
```
3️⃣ Run EF Core Migrations

In Visual Studio → Package Manager Console:
```
Add-Migration InitialCreate -Project OrderManagement.Infrastructure -StartupProject OrderManagement.API
Update-Database
```
4️⃣ Run the API
```
dotnet run --project OrderManagement.API
```
Open Swagger:
```
https://localhost:xxxx/swagger
```
---

📂 Test Project Structure
```
OrderManagementSolution
|
tests
|___ OrderManagement.Tests
| |___ Commands
| |___ Queries
| |___ Repositories
```

🧪 Running Tests

From root directory:
```
dotnet test
```
Tests include:
  - ✔ Command handlers
  - ✔ Query handlers
  - ✔ Repository tests (InMemory DB)
  - ✔ Validation tests


---

🧰 Important Project Highlights

📌 Onion architecture with strict boundaries
Ensures maintainability and testability.

📌 EF Core Fluent API configuration
All entity configurations live under:
Infrastructure/Persistence/Configurations/

📌 Repository pattern
Abstracts data access with:
IOrderRepository → OrderRepository

📌 CQRS + MediatR
All writes/reads are separated into Commands & Queries.

📌 JWT implementation
Token generation through:
JwtTokenService

---

📅 Roadmap / Future Enhancements

 - Refresh Tokens
 - Serilog structured logging
 - Docker support (API + SQL Server)
 - Auto-deploy pipeline (GitHub Actions)
 - Angular/React front-end
 - Pagination, filtering, sorting
 - User management system
 - Multi-tenant support

---

🤝 Contributing

Fork → Branch → Commit → Pull Request
Contributions welcome!

---

📄 License

This project is open-source and free to use.

---

👤 Author

Sebastian Dilshan Pandithasekera
.NET / C# Developer / Web Developer & Technical Business Analyst
Melbourne, Australia

---

⭐ Final Notes

This project was created as a portfolio piece to demonstrate:

Real-world .NET engineering
Clean architecture patterns
Database-driven design
Secure API development
Enterprise-grade testing
Modern coding standards

If you’re reviewing this as a hiring manager or engineer, feel free to reach out!

---

