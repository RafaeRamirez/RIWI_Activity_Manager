# Frontend API Documentation

This guide provides detailed information on how to consume the API from the frontend application.

## 🚀 Getting Started

### Base URL
All API requests should be prefixed with:
`http://localhost:1924/api`

### Authentication
Most endpoints require a valid JWT token. You must include the token in the `Authorization` header of your requests.

**Header Format:**
```http
Authorization: Bearer <your_token>
```

---

## 🔑 Authentication Endpoints

### Login
Authenticate a user and retrieve a JWT token.

- **URL**: `/auth/login`
- **Method**: `POST`
- **Auth Required**: No

#### Request Body
```json
{
  "email": "admin@riwi.io",
  "password": "your_password"
}
```

#### Success Response (200 OK)
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "admin@riwi.io",
  "personId": 1,
  "fullName": "Admin User",
  "role": "Admin"
}
```

#### Error Response (401 Unauthorized)
```json
{
  "message": "Email not found"
}
```

---

## 📅 Events Endpoints

### List All Events
Get a list of all available events.

- **URL**: `/event`
- **Method**: `GET`
- **Auth Required**: Yes

#### Success Response (200 OK)
```json
[
  {
    "eventId": 1,
    "title": "Intro to C# Workshop",
    "description": "A beginner friendly workshop.",
    "eventType": "Workshop",
    "capacity": 30,
    "startAt": "2025-12-22T22:00:00+00:00",
    "endAt": "2025-12-22T23:00:00+00:00",
    "isPublished": true
  }
]
```

### Create Event
Create a new event. Only users with `Admin` role can perform this action.

- **URL**: `/event`
- **Method**: `POST`
- **Auth Required**: Yes (Admin)

#### Request Body
| Field | Type | Required | Description |
|-------|------|----------|-------------|
| title | string | Yes | Event title |
| description | string | No | Event description |
| eventType | string | Yes | e.g., "Workshop", "Meetup" |
| capacity | int | Yes | Max attendees |
| startAt | string | Yes | ISO 8601 Date |
| endAt | string | Yes | ISO 8601 Date |
| locationId | long | No | ID of the location |

**Example:**
```json
{
  "title": "React Masterclass",
  "description": "Advanced patterns in React.",
  "eventType": "Workshop",
  "capacity": 50,
  "startAt": "2025-12-25T10:00:00Z",
  "endAt": "2025-12-25T14:00:00Z",
  "requiresCheckin": true,
  "isPublished": true
}
```

---

## 👥 People Endpoints

### List All People
Get a list of registered users.

- **URL**: `/person`
- **Method**: `GET`
- **Auth Required**: Yes

### Create Person
Register a new user manually.

- **URL**: `/person`
- **Method**: `POST`
- **Auth Required**: Yes

#### Request Body
```json
{
  "email": "newuser@riwi.io",
  "fullName": "New User",
  "role": "Coder",  // Options: "Admin", "Coder"
  "phone": "1234567890"
}
```

---

## 📍 Other Endpoints

### Locations
- `GET /location`: List locations
- `POST /location`: Add location
  ```json
  {
    "name": "Main Hall",
    "address": "123 Tech Street",
    "capacity": 100
  }
  ```

### Speakers
- `GET /speaker`: List speakers
- `POST /speaker`: Add speaker

### Organizations
- `GET /organization`: List organizations

---

## ⚠️ Error Handling

The API uses standard HTTP status codes to indicate success or failure.

| Status Code | Meaning | Description |
|-------------|---------|-------------|
| **200** | OK | Request succeeded. |
| **201** | Created | Resource created successfully. |
| **400** | Bad Request | Invalid input data (check your JSON). |
| **401** | Unauthorized | Invalid or missing JWT token. |
| **403** | Forbidden | You don't have permission (e.g., non-admin trying to create event). |
| **404** | Not Found | The requested resource (ID) does not exist. |
| **500** | Server Error | Something went wrong on the server. |
