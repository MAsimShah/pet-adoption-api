# 🐾 Pet Adoption API - .NET Core 9 Web API

A robust Web API built with **.NET Core 9** using **Clean Architecture**, **JWT Authentication**, **Swagger**, and **Entity Framework Core (Code-First)**. It allows user registration, login, managing pets, handling adoption requests, and image uploads.

---

## 🚀 Features

- 🔐 **User Authentication** (Signup/Login) using **JWT Tokens**
- 👤 **Role-based Identity** system with ASP.NET Core Identity
- 🐶 **Pet CRUD** operations: Create, Read, Update, Delete, Search
- 📦 **Adoption Requests**: Users can add/update/delete their pet adoption requests
- 📸 **Image Upload** support via API (e.g., pet profile images)
- 🧼 **Clean Architecture** with:
  - Application Layer
  - Domain Layer
  - Infrastructure Layer
  - Web API Layer
- 🗃️ **Repository Pattern + Interfaces**
- 🧪 **Swagger/OpenAPI** documentation for easy testing
- 💾 **EF Core Code-First** approach with migrations

---

## 🧱 Architecture

- /PetAdoption.API --> API Layer (Controllers, Middleware, Swagger setup)
- /PetAdoption.Application --> Business Logic (DTOs, Services, Interfaces)
- /PetAdoption.Domain --> Entities, Enums, Domain Logic
- /PetAdoption.Infrastructure --> EF Core, Data Access, Repositories, Identity

## 🛠️ Technologies Used

- .NET 9 (ASP.NET Core Web API)
- Entity Framework Core 9
- ASP.NET Core Identity
- JWT Bearer Authentication
- Swagger / Swashbuckle
- FluentValidation
- AutoMapper
- SQL Server (or SQLite/InMemory for testing)
- Clean Architecture principles

---

## 📸 Image Upload

Images can be uploaded via multipart form data to associate with a pet profile. Uploaded files are saved to a specified directory or cloud storage (configurable).

---

## Set up the database
## 🧰 Code First Migrations

Create Migrations:

- dotnet ef migrations add [migration_comment] -s ..\PetAdoption.API\PetAdoption.API.csproj

Update Database:

- dotnet ef database update -s ..\PetAdoption.API\PetAdoption.API.csproj

---

## Swagger UI

Visit: http://localhost:{port}/

---

### 🔐 JWT Setup
- Tokens are issued during login.
- Add Authorization: Bearer <token> in Swagger or API clients.

---

### 📄 License

Let me know if you want the actual project structure or template code scaffolded for this setup too.

---

### 🧑‍💻 Suggestions & Feedback
This is a personal project and not open to direct contributions at the moment.
However, suggestions, feature requests, and feedback are always welcome!

If you have ideas or want to report issues, feel free to:

📩 Contact me directly via [shahasim190@gmail.com]

📬 Open an issue on the GitHub repository
