# Coworking Reservation System

Un sistema de reservas para espacios de coworking desarrollado con .NET 8.0 y PostgreSQL, siguiendo principios SOLID, inyección de dependencias, y arquitectura en capas.

## Descripción

Este sistema permite la gestión completa de reservas de salas en un centro de coworking, incluyendo:

- Autenticación de usuarios con JWT
- Gestión de salas
- Reserva y cancelación de espacios
- Consulta de disponibilidad
- Registro de auditoría de acciones


## Tecnologías Utilizadas

- .NET 8.0
- PostgreSQL
- Entity Framework Core
- JWT para autenticación
- FluentValidation para validaciones
- Arquitectura en capas (Repositories, Services, Controllers)
- Principios SOLID


## Requisitos Previos

- .NET 8.0 SDK
- PostgreSQL
- Un editor de código (Visual Studio, VS Code, etc.)


## Configuración

1. Clona el repositorio:


```shellscript
git clone https://github.com/EdgarC97/CoworkingReservationSystem
cd CoworkingReservationSystem
```

2. Crea un archivo `.env` en la raíz del proyecto basado en `.env.example`:


```plaintext
DATABASE_CONNECTION=Host=localhost;Database=coworking_db;Username=postgres;Password=yourpassword
JWT_SECRET=your_very_long_and_secure_secret_key_here
JWT_ISSUER=CoworkingReservationSystem
JWT_AUDIENCE=CoworkingUsers
JWT_EXPIRY_MINUTES=60
```

3. Restaura los paquetes NuGet:


```shellscript
dotnet restore
```

## Ejecución
4. Ejecuta la aplicacion y esta creará las migraciones y los seeders en la base de datos:


```shellscript
dotnet run 
```


La API estará disponible en:

- [http://localhost:5030](http://localhost:5030) (HTTP)


## Estructura del Proyecto

```plaintext
CoworkingReservationSystem/
├── Controllers/           # Controladores de la API
├── Data/                  # Capa de acceso a datos
│   ├── Configurations/    # Configuraciones de entidades
│   ├── Repositories/      # Implementaciones de repositorios
│   └── Seeders/           # Clases para poblar la base de datos
├── DTOs/                  # Objetos de transferencia de datos
│   ├── Requests/          # DTOs para solicitudes
│   └── Responses/         # DTOs para respuestas
├── Interfaces/            # Interfaces
│   ├── Repositories/      # Interfaces de repositorios
│   └── Services/          # Interfaces de servicios
├── Middlewares/           # Middlewares personalizados
├── Models/                # Modelos de dominio
├── Services/              # Implementaciones de servicios
├── Utils/                 # Utilidades y helpers
└── Validators/            # Validadores de FluentValidation
```

## Endpoints de la API

### Autenticación

- `POST /api/register` - Registro de usuario
- `POST /api/login` - Inicio de sesión


### Salas

- `POST /api/rooms` - Crear una sala
- `GET /api/rooms` - Obtener todas las salas
- `GET /api/rooms/{id}` - Obtener una sala por ID
- `GET /api/rooms/available` - Consultar salas disponibles en un rango de tiempo


### Reservas

- `POST /api/bookings` - Crear una reserva
- `PUT /api/bookings/{id}/cancel` - Cancelar una reserva
- `GET /api/bookings/{id}` - Obtener una reserva por ID
- `GET /api/users/{id}/bookings` - Obtener reservas de un usuario


### Auditoría

- `GET /api/audits` - Obtener todos los registros de auditoría
- `GET /api/audits/user/{userId}` - Obtener auditorías por usuario
- `GET /api/audits/entity/{entityName}` - Obtener auditorías por entidad


## Colección de Postman

Se incluye una colección de Postman para probar todos los endpoints. Para usarla:

1. Importa el archivo `CoworkingReservationSystem.postman_collection.json` en Postman
2. Configura la variable de entorno `baseUrl` a `https://localhost:5030` (o el puerto que estés usando)
3. Ejecuta el endpoint de registro para crear un usuario
4. Ejecuta el endpoint de login para obtener un token JWT
5. El token se guardará automáticamente y se usará en las siguientes solicitudes


## Datos de Prueba

Si ejecutaste el seeding, puedes usar las siguientes credenciales para probar:

- Email: [admin@example.com](mailto:admin@example.com)
- Password: Admin123!


## Características Principales

1. **Autenticación Segura**: Implementación de JWT para autenticación de usuarios.
2. **Validación de Reservas**: Verificación de disponibilidad y prevención de solapamientos.
3. **Auditoría Completa**: Registro detallado de todas las acciones realizadas en el sistema.
4. **Arquitectura en Capas**: Separación clara de responsabilidades siguiendo principios SOLID.
5. **Validaciones Robustas**: Uso de FluentValidation para validar entradas.


## Licencia

Este proyecto está licenciado bajo la Licencia MIT - ver el archivo LICENSE para más detalles.