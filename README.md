# 🚀 Employee Management System API

A Full Stack Employee Management System built using:

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server
- JWT Authentication
- Role-Based Authorization (Admin / User)
- Layered Architecture

---

## 📌 Overview

This API provides a secure backend system for managing:

- Employees
- Departments
- Salaries
- Attendance
- Users & Roles

The system implements JWT-based authentication with role-based authorization to ensure secure access control.

---

## 🏗️ Architecture

This project follows a clean layered architecture:

Controllers → Services → Repositories → Database (EF Core)

### Layers Explained

- Controllers → Handle HTTP requests and responses
- Services → Business logic implementation
- Repositories → Database access layer
- DTOs → Data Transfer Objects (prevent direct entity exposure)
- AutoMapper → Entity to DTO mapping
- JWT → Authentication & Authorization

---

## 🗄️ Database Schema

The system uses SQL Server with the following tables:

- Roles
- Users
- Departments
- Employees
- Salaries
- Attendance

SQL Server Management Studio (SSMS) is used for database management.

---

## 🔐 Authentication & Authorization

### JWT Based Authentication

- Secure login endpoint
- Token expiration: 2 Hours
- JWT Claims:
  - UserId
  - RoleId
  - Role

### Role-Based Authorization

**Admin**
- Manage Employees
- Manage Departments
- Manage Salaries
- Create Users
- Assign Roles

**User**
- View own profile
- View own attendance
- View own salary

---

## 🔒 Security Best Practices

- Password Hashing using BCrypt
- JWT Token Validation
- Role-based policies
- Soft Delete (IsActive, IsDeleted)
- LastLoginDate update on login
- DTO usage (no direct entity exposure)
- Global Exception Handling
- Input validation

---

## 📦 Technologies Used

- .NET 8
- SQL Server 2019+
- EF Core 8
- AutoMapper
- BCrypt.Net

---

## 📁 Project Structure

EmployeeManagement.API  
│  
├── Controllers  
├── Services  
│   ├── Interfaces  
├── Repositories  
│   ├── Interfaces  
├── DTOs  
├── Models  
├── Helpers  
├── Mapping  
└── Program.cs  

---

## ⚙️ Setup Instructions

### 1️⃣ Clone Repository

```bash
git clone https://github.com/yourusername/EmployeeManagement.API.git
cd EmployeeManagement.API
```

---

### 2️⃣ Configure Database

Update `appsettings.json`:

```json
"ConnectionStrings": {
  "Default": "Server=.;Database=EmployeeManagementDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

---

### 3️⃣ Configure JWT

```json
"Jwt": {
  "Key": "YourSuperSecretKeyHere123456",
  "Issuer": "EmployeeAPI",
  "Audience": "EmployeeAPIUsers"
}
```

---

### 4️⃣ Run Migration (If Using Code First)

```bash
Add-Migration InitialCreate
Update-Database
```

---

### 5️⃣ Run Project

```bash
dotnet run
```

API will run at:

```
https://localhost:5001
```

---

## 📌 API Endpoints

### Authentication

```
POST /api/auth/login
```

### Employees (Admin Only)

```
GET    /api/employees
POST   /api/employees
PUT    /api/employees/{id}
DELETE /api/employees/{id}
```

### Departments

```
GET /api/departments
```

### Salaries

```
GET /api/salaries
```

### Attendance

```
GET /api/attendance
```

---

## 🧪 Testing

You can test APIs using:

- Postman
- Swagger UI
- Thunder Client

Swagger URL:

```
https://localhost:5001/swagger
```

---

## 📈 Future Improvements

- Refresh Token Implementation
- Pagination & Filtering
- Logging (Serilog)
- Caching (Redis)
- Docker Support
- Unit Testing
- CI/CD Pipeline

---

## 👨‍💻 Author

Developed by: Your Name  
Role: Full Stack Developer (.NET + Angular)

---

## 📜 License

This project is licensed under the MIT License.
