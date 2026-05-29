
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
```

### 2. Backend Setup

#### Configure Environment Variables
Create a `.env` file in the `Backend` folder:
```env
DB_SERVER=localhost
DB_NAME=doctor_appointment_db
DB_USER=your_mysql_user
DB_PASSWORD=your_mysql_password
JWT_SECRET_KEY=YourSuperSecretKeyForProduction
```

#### Install Dependencies & Run Migrations
```bash
cd Backend
dotnet restore
dotnet ef database update
dotnet run
```

The backend API will start at `https://localhost:5001` (or check console output for the actual port).

### 3. Frontend Setup

```bash
cd Frontend
npm install
npm run dev
```

The frontend will start at `http://localhost:5173`.

## 🔌 API Endpoints

### Authentication
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/api/auth/register` | Register a new patient |
| POST | `/api/auth/login` | Login and receive JWT token |

### Doctors
| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/doctors` | Get all doctors | No |
| GET | `/api/doctors/{id}` | Get doctor by ID | No |
| GET | `/api/doctors/search` | Search doctors by speciality/clinic | No |
| POST | `/api/doctors` | Create new doctor | Yes |
| PUT | `/api/doctors/{id}` | Update doctor | Yes |
| DELETE | `/api/doctors/{id}` | Delete doctor | Yes |

### Appointments
| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/appointment` | Get all appointments | Yes |
| GET | `/api/appointment/{id}` | Get appointment by ID | Yes |
| POST | `/api/appointment` | Create new appointment | Yes |
| PUT | `/api/appointment/{id}` | Update appointment | Yes |
| DELETE | `/api/appointment/{id}` | Cancel appointment | Yes |

## 📊 Database Schema

### Main Entities
- **Patients:** User profiles with authentication credentials and personal information
- **Doctors:** Medical professionals with specialities and clinic affiliations
- **Appointments:** Booking records linking patients and doctors
- **Clinics:** Medical facilities where doctors practice
- **Specialities:** Medical specialties (e.g., Cardiology, Pediatrics)
- **Categories:** Appointment categories (e.g., Consultation, Follow-up)

### Relationships
- One Patient → Many Appointments
- One Doctor → Many Appointments
- One Clinic → Many Doctors
- One Speciality → Many Doctors

## 📁 Project Structure

```
Doctor_appointment_app/
├── Backend/
│   ├── Controllers/        # API endpoints
│   │   ├── AppointmentController.cs
│   │   ├── AuthController.cs
│   │   └── DoctorsController.cs
│   ├── Models/            # Database entities
│   │   ├── Appointment.cs
│   │   ├── Doctor.cs
│   │   ├── Patient.cs
│   │   ├── Clinic.cs
│   │   ├── Speciality.cs
│   │   └── Category.cs
│   ├── DTOs/              # Data Transfer Objects
│   ├── Data/              # DbContext and configurations
│   ├── Services/          # Business logic (JWT service)
│   ├── Migrations/        # EF Core migrations
│   └── Program.cs         # Application entry point
│
└── Frontend/
    ├── src/
    │   ├── components/    # React components
    │   ├── app/          # Application logic
    │   ├── lib/          # Utilities and helpers
    │   └── types/        # Type definitions
    ├── public/           # Static assets
    └── package.json      # Dependencies
```

## 🔐 Authentication Flow

1. Patient registers via `/api/auth/register`
2. Credentials are stored with hashed password (BCrypt)
3. Patient logs in via `/api/auth/login`
4. Server returns JWT token
5. Client includes token in Authorization header for protected routes
6. Server validates token on each protected request

Example Authorization Header:
```
Authorization: Bearer <your-jwt-token>
```

## 🧪 Testing the API

You can use the included `Backend.http` file with the REST Client extension in VS Code, or import the endpoints into Postman/Insomnia.

### Example: Register a New Patient
```http
POST https://localhost:5001/api/auth/register
Content-Type: application/json

{
  "firstName": "John",
  "lastName": "Doe",
  "email": "john.doe@example.com",
  "password": "SecurePass123!",
  "phone": "1234567890"
}
```

### Example: Login
```http
POST https://localhost:5001/api/auth/login
Content-Type: application/json

{
  "email": "john.doe@example.com",
  "password": "SecurePass123!"
}
```

### Example: Create an Appointment (Requires Auth)
```http
POST https://localhost:5001/api/appointment
Content-Type: application/json
Authorization: Bearer <your-jwt-token>

{
  "patientId": 1,
  "doctorId": 1,
  "clinicId": 1,
  "categoryId": 1,
  "appointmentDate": "2026-06-15T10:00:00",
  "timeSlot": "10:00:00",
  "notes": "Regular checkup"
}
```

## 🚧 Current Development Status

This project demonstrates my skills in:
- ✅ Building RESTful APIs with ASP.NET Core
- ✅ Database design and Entity Framework Core
- ✅ JWT authentication and authorization
- ✅ Secure password handling
- ✅ CRUD operations and data validation
- 🔄 Frontend development with React (in progress)

## 🤝 Contributing

This is a personal portfolio project, but suggestions and feedback are welcome! Feel free to open an issue or reach out directly.

## 📝 License

This project is open source and available under the [MIT License](LICENSE).

## 📧 Contact

For questions or collaboration opportunities:
- **Email:** [hj.loudbirds@gmail.com]
- **GitHub:** [RikatoariLB]
- **LinkedIn:** [https://www.linkedin.com/in/hans-jonas-engebretsen-t%C3%B8fte-45539b2b6/]

---

**Status:** 🟡 Work in Progress | **Last Updated:** May 2026