# Jira Clone (CLI)

A lightweight, interactive console-based task management system built to demonstrate Enterprise-grade software architecture patterns in .NET. 

## Architecture & Tech Stack

This project strictly adheres to **Clean Architecture** and **Domain-Driven Design (DDD)** principles, ensuring a highly decoupled, testable, and maintainable codebase.

* **Platform:** .NET 9 / C#
* **Architecture:** Clean Architecture, Rich Domain Model, Use Cases (Interactors)
* **Data Access:** Entity Framework Core, PostgreSQL (Npgsql)
* **Patterns applied:** * Repository Pattern (Abstracting data persistence)
    * Dependency Injection (Scoped containers per CLI command)
    * REPL (Read-Eval-Print Loop) Router for presentation
    * Persistence Ignorance (Domain has no knowledge of EF Core or DB schemas)

## Core Features

* **Rich Domain:** Encapsulated state mutations, hidden setters, and domain validation. No Anemic Domain Models.
* **Memory-Safe I/O:** Custom CLI router creates isolated DI scopes per command to prevent `DbContext` memory leaks and threading issues.
* **Fluent API Mapping:** Database schema configuration is strictly isolated in the Infrastructure layer using EF Core Fluent API (snake_case convention applied).

## Quick Start

1. Set up your PostgreSQL connection string in `appsettings.json`.
2. Apply database migrations:
   ```bash
   dotnet ef database update --project Infrastructure --startup-project CLI
