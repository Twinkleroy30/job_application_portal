# JobPortal

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 16.2.16.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.
# 💼 Job Portal

A full-featured **Job Portal** web application built with Angular 16 (with Angular Material) for the frontend, Node.js (TypeScript) and Express for the backend, and MySQL as the database. Users can register, log in, view jobs, apply, save favorites, and manage their profile. Admins can post, edit, and delete jobs.

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

## 🔧 Tech Stack

| Layer      | Tech                           |
|------------|--------------------------------|
| Frontend   | Angular 16, Angular Material   |
| Backend    | Node.js, Express, TypeScript   |
| Database   | MySQL                          |
| Tools      | MySQL Workbench, Postman, JWT, Nodemailer, bcrypt |

---

## 📂 Project Structure

```bash
JobPortal/
├── frontend/         # Angular 16 frontend
│   └── src/app/
├── backend/          # Node.js + Express backend
│   └── src/
│       ├── controllers/
│       ├── routes/
│       ├── models/
│       └── config/
├── README.md
