# Job  Application Portal

A full-featured **Job Portal** web application with a modern frontend and robust backend. Users can register, log in, view jobs, apply, save favorites, and manage their profile. Admins can post, edit, and delete jobs.

---

## Tech Stack

| Layer      | Technology                    |
|------------|------------------------------|
| Frontend   | Angular 16, Angular Material |
| Backend    | .NET 9, ASP.NET Core         |
| Database   | SQLite                       |
| Tools      | Postman, JWT, Nodemailer, bcrypt |

---

## Project Structure

```
JobPortalBackend/                # Backend (.NET 9, ASP.NET Core)
├── Controllers/                # API controllers
├── Data/                       # Database context and seed data
├── Migrations/                 # EF Core migrations
├── Models/                     # Data models
├── Properties/                 # Project properties
├── Program.cs                  # Backend entry point
├── appsettings.json            # Configuration files
├── JobPortalBackend.csproj     # Project file
src/                           # Frontend (Angular 16)
├── app/
│   ├── components/             # Angular components (home, profile, job-form, favorites, etc.)
│   ├── guards/                 # Route guards
│   ├── models/                 # Frontend data models
│   ├── services/               # Services for API calls and business logic
│   ├── app.module.ts           # Angular module
│   ├── app-routing.module.ts   # Routing configuration
│   ├── app.component.*         # Root component files
├── assets/                     # Static assets (images, icons)
├── environments/               # Environment configuration files
├── index.html                  # Main HTML file
├── main.ts                     # Angular bootstrap file
├── styles.scss                 # Global styles
angular.json                   # Angular CLI configuration
package.json                   # Frontend dependencies and scripts
tsconfig.json                  # TypeScript configuration
```

---

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (includes npm)
- Angular CLI (optional, for development): `npm install -g @angular/cli`

### Running the Backend

1. Open a terminal and navigate to the backend directory:

   ```bash
   cd JobPortalBackend
   ```

2. Restore dependencies and run the backend server:

   ```bash
   dotnet restore
   dotnet run
   ```

3. The backend API will be available at `http://localhost:5297` (or as configured).

### Running the Frontend

1. Open a terminal and navigate to the frontend directory:

   ```bash
   cd src
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Start the Angular development server:

   ```bash
   ng serve
   ```

4. Open your browser and navigate to `http://localhost:4200/`.

---

## ✨ Features

### 👤 Authentication & User Management
- Register & login (OTP-based login for regular users)
- Admin login with email/password
- Route protection for unauthorized users
- Session persistence with auth token
- Dynamic navbar showing user info after login

### 📄 Job Management
- View all job listings with:
  - Pagination
  - Search by job title/company
  - Filter by location and job type
- Apply to jobs
- Save jobs to favorites
- Admin:
  - Post a new job
  - Edit existing jobs
  - Delete job postings

### 🔖 Favorites & Applications
- Save jobs to favorites
- Apply for jobs
- View saved & applied jobs in the user profile
- Favorites and applications are stored in the database

### 🧾 Profile Page
- View user details (name, email)
- List of favorite jobs
- List of applied jobs
- Profile loaded from the database

### 🖼️ UI/UX
- Angular Material UI components
- Responsive design
- Clean layout and mobile friendly
- Footer: _Developed by R Yashwanth_

---

## Database

- Uses SQLite as the database engine
- Entity Framework Core for ORM and migrations
- Migrations are located in `JobPortalBackend/Migrations`

---

## Additional Notes

- Use Postman or similar tools to test backend API endpoints
- Environment-specific settings can be configured in `appsettings.json` and Angular environment files
- For production deployment, build the Angular app with `ng build --prod` and configure backend to serve static files

---
