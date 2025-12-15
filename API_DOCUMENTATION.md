# API Documentation

## Overview
This API is built with .NET 8 and uses PostgreSQL as the database. It is fully dockerized for easy deployment.

## Getting Started

### Prerequisites
- Docker
- Docker Compose

### Running the Application
1.  Navigate to the project root.
2.  Run the following command:
    ```bash
    docker-compose up -d --build
    ```
3.  The API will be available at `http://localhost:8080`.
4.  The Database will be available on port `5432`.

## API Endpoints

### Authentication
- **Login**: `POST /api/auth/login`
    - **Body**:
        ```json
        {
            "email": "admin@riwi.io",
            "password": "your_password"
        }
        ```
    - **Default Admin Credentials**:
        - Email: `admin@riwi.io`
        - Password: `123456`

### Swagger UI
The API documentation is available via Swagger UI at:
- `http://localhost:8080/swagger/index.html`

## Database Seeding
The application automatically seeds the database with default users and events on startup if the tables are empty.
- **Admin User**: `admin@riwi.io`
- **Regular Users**: `user1@riwi.io`, `user2@riwi.io`
- **Default Password**: `123456`

## Frontend Integration
The API is configured with CORS to allow requests from:
- `http://localhost:5173` (Vite default)
- `http://localhost:3000` (Create React App default)
- `http://localhost:5174`
- `http://localhost:4173`

Ensure your frontend application is running on one of these ports or update the CORS policy in `Program.cs`.
