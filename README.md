
# 🏥 Doctor Appointment Booking System

A full-stack web application for managing doctor appointments, built with ASP.NET Core (.NET 10) and React. This system allows patients to register, search for doctors, and book appointments at various clinics.

> **Note:** This project is currently a work in progress and under active development.

## ✨ Features

### Implemented
- ✅ **User Authentication & Authorization**
  - Patient registration and login with JWT authentication
  - Secure password hashing with BCrypt
  - Token-based session management

- ✅ **Doctor Management**
  - Browse and search doctors by speciality and clinic
  - View doctor details and availability
  - CRUD operations for doctor records

- ✅ **Appointment System**
  - Create, update, and cancel appointments
  - Appointment status tracking (Scheduled, Completed, Cancelled)
  - Time slot management
  - Patient-Doctor-Clinic relationship tracking

- ✅ **Database Schema**
  - Patients with comprehensive profile information
  - Doctors with specialities and clinic affiliations
  - Clinics and medical categories
  - Relational data integrity with Entity Framework Core

### Planned Enhancements
- 🔄 Frontend UI implementation with React
- 🔄 Real-time appointment notifications
- 🔄 Doctor availability calendar
- 🔄 Patient medical history tracking
- 🔄 Admin dashboard for clinic management
- 🔄 Email notifications for appointment confirmations

## 🛠️ Tech Stack

### Backend
- **Framework:** ASP.NET Core 10.0 (Web API)
- **Database:** MySQL with Entity Framework Core 10.0
- **Authentication:** JWT Bearer Token
- **Password Security:** BCrypt.Net
- **Environment Management:** DotNetEnv
- **API Documentation:** OpenAPI (Swagger)

### Frontend
- **Framework:** React 19.2
- **Build Tool:** Vite 8.0
- **Language:** JavaScript (ES6+)
- **Linter:** ESLint 10.0

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v18 or higher)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) (v8.0 or higher)
- A code editor (VS Code, Visual Studio, etc.)

## 🚀 Installation & Setup

### 1. Clone the Repository
```bash
git clone <your-repository-url>
cd Doctor_appointment_app