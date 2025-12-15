# Documentación de la API para Frontend

Esta guía proporciona información detallada sobre cómo consumir la API desde la aplicación frontend.

## 🚀 Comenzando

### URL Base
Todas las peticiones a la API deben tener el prefijo:
`http://localhost:8080/api`

### Autenticación
La mayoría de los endpoints requieren un token JWT válido. Debes incluir el token en el encabezado `Authorization` de tus peticiones.

**Formato del Encabezado:**
```http
Authorization: Bearer <tu_token>
```

---

## 🔑 Endpoints de Autenticación

### Iniciar Sesión (Login)
Autentica a un usuario y obtiene un token JWT.

- **URL**: `/auth/login`
- **Método**: `POST`
- **Requiere Auth**: No

#### Cuerpo de la Petición
```json
{
  "email": "admin@riwi.io",
  "password": "tu_contraseña"
}
```

#### Respuesta Exitosa (200 OK)
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "email": "admin@riwi.io",
  "personId": 1,
  "fullName": "Admin User",
  "role": "Admin"
}
```

#### Respuesta de Error (401 Unauthorized)
```json
{
  "message": "Email no encontrado"
}
```

---

## 📅 Endpoints de Eventos

### Listar Todos los Eventos
Obtiene una lista de todos los eventos disponibles.

- **URL**: `/event`
- **Método**: `GET`
- **Requiere Auth**: Sí

#### Respuesta Exitosa (200 OK)
```json
[
  {
    "eventId": 1,
    "title": "Taller de Intro a C#",
    "description": "Un taller para principiantes.",
    "eventType": "Workshop",
    "capacity": 30,
    "startAt": "2025-12-22T22:00:00+00:00",
    "endAt": "2025-12-22T23:00:00+00:00",
    "isPublished": true
  }
]
```

### Crear Evento
Crea un nuevo evento. Solo los usuarios con rol `Admin` pueden realizar esta acción.

- **URL**: `/event`
- **Método**: `POST`
- **Requiere Auth**: Sí (Admin)

#### Cuerpo de la Petición
| Campo | Tipo | Requerido | Descripción |
|-------|------|-----------|-------------|
| title | string | Sí | Título del evento |
| description | string | No | Descripción del evento |
| eventType | string | Sí | Ej: "Workshop", "Meetup" |
| capacity | int | Sí | Capacidad máxima |
| startAt | string | Sí | Fecha ISO 8601 |
| endAt | string | Sí | Fecha ISO 8601 |
| locationId | long | No | ID de la ubicación |

**Ejemplo:**
```json
{
  "title": "Masterclass de React",
  "description": "Patrones avanzados en React.",
  "eventType": "Workshop",
  "capacity": 50,
  "startAt": "2025-12-25T10:00:00Z",
  "endAt": "2025-12-25T14:00:00Z",
  "requiresCheckin": true,
  "isPublished": true
}
```

---

## 👥 Endpoints de Personas (Usuarios)

### Listar Todas las Personas
Obtiene una lista de usuarios registrados.

- **URL**: `/person`
- **Método**: `GET`
- **Requiere Auth**: Sí

### Crear Persona
Registra un nuevo usuario manualmente.

- **URL**: `/person`
- **Método**: `POST`
- **Requiere Auth**: Sí

#### Cuerpo de la Petición
```json
{
  "email": "nuevousuario@riwi.io",
  "fullName": "Nuevo Usuario",
  "role": "Coder",  // Opciones: "Admin", "Coder"
  "phone": "1234567890"
}
```

---

## 📍 Otros Endpoints

### Ubicaciones (Locations)
- `GET /location`: Listar ubicaciones
- `POST /location`: Agregar ubicación
  ```json
  {
    "name": "Salón Principal",
    "address": "Calle Tech 123",
    "capacity": 100
  }
  ```

### Ponentes (Speakers)
- `GET /speaker`: Listar ponentes
- `POST /speaker`: Agregar ponente

### Organizaciones
- `GET /organization`: Listar organizaciones

---

## ⚠️ Manejo de Errores

La API utiliza códigos de estado HTTP estándar para indicar éxito o fracaso.

| Código | Significado | Descripción |
|--------|-------------|-------------|
| **200** | OK | Petición exitosa. |
| **201** | Created | Recurso creado exitosamente. |
| **400** | Bad Request | Datos de entrada inválidos (revisa tu JSON). |
| **401** | Unauthorized | Token JWT faltante o inválido. |
| **403** | Forbidden | No tienes permiso (ej: no-admin intentando crear evento). |
| **404** | Not Found | El recurso solicitado (ID) no existe. |
| **500** | Server Error | Algo salió mal en el servidor. |
