# InstantHire Freelance Platform Backend

## Overview

InstantHire is a console-based backend system that simulates a freelance marketplace platform where clients can post projects and freelancers can submit bids.

The project is built using **C# (.NET 10)**, **Entity Framework Core 10**, and **SQL Server**, following clean OOP principles, domain-driven design concepts, and modern backend architecture practices.

It demonstrates real-world backend development concepts including business rules enforcement, event-driven programming, LINQ reporting, asynchronous programming, and database-first thinking using EF Core Code First approach.

=====================================================

# Technologies Used

* C#
* .NET 10
* Entity Framework Core 10
* SQL Server
* LINQ
* Async / Await
* Git & GitHub

=====================================================

# Project Structure


InstantHire
│
├── Abstractions
│   ├── FreelancerBase.cs
│   └── IReviewable.cs
│
├── Domain
│   ├── Entities
│   ├── Enums
│   ├── Exceptions
│   └── DTOs
│
├── Data
│   ├── AppDbContext.cs
│   ├── AppDbContextFactory.cs
│   └── Migrations
│
├── Repositories
│   └── Repository.cs
│
├── Services
│   ├── BidService.cs
│   ├── ReportService.cs
│   ├── NotificationService.cs
│   ├── ProjectService.cs
│   ├── ProjectAppService.cs
│   └── InputService.cs
│
└── Program.cs


=====================================================

# Database Design

## Main Entities

* Client
* Freelancer
* Project
* Bid
* Skill
* Review

=====================================================

## Relationships

### One-To-Many

* One Client → Many Projects
* One Freelancer → Many Bids
* One Project → Many Bids
* One Freelancer → Many Reviews

### One-To-One

* One Project → One Review

### Many-To-Many

Freelancers ↔ Skills

=====================================================

# OOP Concepts Applied

## Abstract Class

`FreelancerBase`

Defines shared freelancer properties:

* FullName
* Specialty
* HourlyRate

Includes:


GetProfileSummary()


as an abstract method.

=====================================================
## Interface

`IReviewable`

Enforces review behavior:


CanReceiveReview()
GetReviewSummary()


Implemented by Freelancer.

=====================================================

## Encapsulation

Project status is protected:


public ProjectStatus Status { get; private set; }


Status changes only through domain methods:

* AcceptBid()
* StartProgress()
* MarkCompleted()

=====================================================

# Repository Pattern

Generic Repository:


Repository<T>


Supports:

* AddAsync
* GetByIdAsync
* GetAllAsync
* Delete
* SaveChangesAsync

=====================================================

# Business Rules

## Bid Rules

* Bid amount must be > 0
* Only one accepted bid per project
* Reject other bids automatically on acceptance

## Project Rules

* Accepting a bid sets project to InProgress
* Status cannot be modified directly

## Review Rules

* Rating must be between 1 and 5
* Reviews are linked to completed projects
* Freelancer reputation is computed dynamically

=====================================================

# Events & Notifications

## OnBidAccepted

Triggered when a bid is accepted:

* Project Title
* Freelancer Name
* Amount
* Status update

=====================================================

## OnLowBudgetWarning

Triggered when bid exceeds 90% of project budget.

Used to warn client before accepting bid.

=====================================================

# Custom Exceptions

* DuplicateAcceptanceException
* InvalidBidException
* FreelancerNotFoundException
* InvalidRatingException

=====================================================

# LINQ Reports

## Top Freelancers

* Average rating
* OrderByDescending
* Take

## Unbid Projects

* Where + Any

## High Bids

* Percentage-based filtering

## Skill Search

* Nested Any()

## Client Spending

* GroupBy + Sum + Select

=====================================================

# Entity Framework Core 10

* Code First
* Migrations
* Fluent API
* SQL Server
* Async queries
* AsNoTracking for performance
* DeleteBehavior.Restrict for safety

=====================================================

# Console Features

* Create Clients
* Create Projects
* Submit Bids
* Accept Bids
* Add Reviews
* Run Reports
* Event Notifications

=====================================================

# How to Run


git clone https://github.com/Ahmed-Zein3/InstantHire-Freelance-Platform.git



dotnet restore
dotnet run


=====================================================

# Learning Outcomes

This project demonstrates:

* Advanced OOP design
* Clean architecture thinking
* EF Core relationships
* LINQ querying
* Async programming
* Event-driven design
* Exception handling
* Real-world backend simulation

=====================================================

# Author

Ahmed Zein
Software Engineer
GitHub: https://github.com/Ahmed-Zein3
