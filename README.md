# Inventory Management System
# 🏭 RoboostTask - Inventory Management System

Inventory Management System is a modular inventory management system built with **ASP.NET Core** that enables businesses to:

- Track products
- Manage stock across multiple warehouses
- Process inventory transactions
- Generate insightful reports
- Handle role-based access control

Built using **clean architecture principles** and modern design patterns like **CQRS**, **Repository**, **Mediator**, and **Orchestrator**.

---

## 📌 Table of Contents
- [Purpose and Scope](#purpose-and-scope)
- [Technology Stack](#technology-stack)
- [Key Features](#key-features)
- [Architecture Overview](#architecture-overview)
- [Domain Model](#domain-model)
- [Design Patterns](#design-patterns)
- [How to Run](#how-to-run)
- [Controllers Overview](#controllers-overview)

---

## 🎯 Purpose and Scope

Inventory Management System offers:

- ✅ **Product Management** (CRUD)
- 🔄 **Inventory Transactions** (Add, Delete, Transfer)
- 🏬 **Warehouse Management**
- 🔔 **Low Stock Alerts**
- 📊 **Reporting with Excel Export**
- 🔐 **Secure JWT Authentication & Role-Based Authorization**

---

## 🧱 Technology Stack

| Category           | Technology                  | Purpose                         |
|--------------------|------------------------------|----------------------------------|
| Framework          | ASP.NET Core 8.0             | Web API Framework               |
| ORM                | Entity Framework Core 8.0    | Database Operations             |
| Database           | SQL Server                   | Relational Data Storage         |
| Authentication     | JWT Bearer, ASP.NET Identity | Security & User Management      |
| API Docs           | Swagger / OpenAPI            | Interactive Documentation       |
| Reporting          | EPPlus (v6.2.1)              | Excel Export                    |
| Email Notifications| MailKit / MimeKit (v4.12.0)  | Sending Stock Alerts            |
| Patterns Used      | MediatR (v8.0.0)             | CQRS, Mediator, Orchestrator    |

---

## ✨ Key Features

### ✅ Inventory Management
- `POST /add-stock`: Add product quantity to a warehouse
- `DELETE /delete-stock`: Remove stock from a warehouse
- `POST /transfer-stock`: Transfer stock between warehouses
- `GET /available-stocks`: View current inventory levels

### 📊 Reporting System
- `GET /low-stock`: View low stock items
- `GET /transaction-history`: Filter inventory transaction logs
- `GET /low-stock/excel`: Download Excel report for low stock
- `GET /transaction-history/excel`: Download Excel transaction report

### 🔐 Authentication & Authorization
- JWT Bearer Auth
- Role-based controller-level authorization: `[Authorize(Roles = "Admin")]`
- Claims-based access using `User.FindFirstValue(ClaimTypes.NameIdentifier)`

---

## 🧠 Architecture Overview

- The system is divided into well-defined layers:
Controllers (API Layer)
│
├── Services / Orchestrators (Business Logic)
│
├── Command / Query Handlers (CQRS + MediatR)
│
└── Repositories (Data Access Layer)


### Example Components:
- `InventoryTransactionsController`
- `ReportsController`
- `AddStockOrchestratorHandler`
- `DeleteStockOrchestratorHandler`
- `IProductRepository`, `IStockRepository`, `IInventoryTransactionRepository`

---

## 🗃️ Domain Model

```text
PRODUCT
- Id : Guid
- Name : string
- Description : string
- Price : decimal
- Quantity : int
- LowStockThreshold : int

STOCK
- Id : Guid
- ProductId : Guid
- WarehouseId : Guid
- QuantityInStock : int

INVENTORY_TRANSACTION
- Id : Guid
- ProductId : Guid
- TransactionType : string (Add/Delete/Transfer)
- Quantity : int
- Date : DateTime
- PerformedByUserId : string
- SourceWarehouseId : Guid?
- DestinationWarehouseId : Guid?

WAREHOUSE
- Id : Guid
- Name : string
- Location : string

USER
- Managed by ASP.NET Identity
```

## 🧩 Design Patterns Used
    - CQRS: Clear separation of read/write operations
    
    - Mediator: Used via MediatR
    
    - Repository Pattern: Abstracts data access
    
    - Orchestrator Pattern: Coordinates multiple actions (e.g., add/delete stock flows)

##🚀 How to Run:

    1-Clone the repository
    
    2-Configure your database connection string in appsettings.json
    
    3-Run database migrations to create the schema
    
    4-Build and run the ASP.NET Core Web API project
    
    5-Access Swagger UI at /swagger to explore and test API endpoin
## Controllers Overview:
    - AccountController: Handles authentication and user management
    
    - ProductsController: Manages product CRUD operations
    
    - InventoryTransactionsController: Handles stock additions, deletions, and transfers
    
    - ReportsController: Provides reporting endpoints and Excel export
    
    - NotificationController: Manages notifications such as low stock alerts

