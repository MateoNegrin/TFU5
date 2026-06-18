# Truck & Roll - Backend API

API REST que implementa los dos casos de uso principales de TFU5:
- **CU1** — Registrar pedido
- **CU4** — Consultar estado del pedido

## Stack Tecnológico

- **Lenguaje:** C#
- **Framework:** .NET 10
- **Persistencia:** In-memory
- **API Documentation:** Swagger/OpenAPI

## Estructura del Proyecto

```
TruckAndRoll/
├── TruckAndRoll.API/              ← Proyecto principal (Web API)
│   ├── Controllers/
│   ├── Program.cs
│   └── appsettings.json
│
├── TruckAndRoll.Application/      ← Servicios / lógica de negocio
│   ├── Services/
│   ├── Interfaces/
│   └── DTOs/
│
├── TruckAndRoll.Domain/           ← Entidades y enums del dominio
│   ├── Entities/
│   ├── Enums/
│   └── Interfaces/
│
└── TruckAndRoll.Infrastructure/   ← Persistencia (in-memory)
    └── Repositories/
```

## Requisitos

- .NET 10 SDK o superior
- Una terminal (PowerShell, cmd, o bash)

## Instalación y ejecución

### 1. Restaurar dependencias
```bash
dotnet restore
```

### 2. Compilar la solución
```bash
dotnet build
```

### 3. Ejecutar la API
```bash
cd TruckAndRoll.API
dotnet run
```

Por defecto, la API estará disponible en:
- **API:** `http://localhost:5042`
- **Swagger UI:** `http://localhost:5042/swagger/index.html`

## Endpoints principales

### 1. Listar productos (menú)
```http
GET /api/productos
```

**Respuesta:**
```json
[
  { "id": 1, "nombre": "Hamburguesa", "precio": 350, "descripcion": "Hamburguesa clásica" },
  { "id": 2, "nombre": "Papas fritas", "precio": 150, "descripcion": "Papas fritas crujientes" },
  { "id": 3, "nombre": "Bebida", "precio": 100, "descripcion": "Bebida fría" }
]
```

### 2. Crear pedido (CU1)
```http
POST /api/pedidos
Content-Type: application/json

{
  "lineas": [
    { "productoId": 1, "cantidad": 2 },
    { "productoId": 2, "cantidad": 1 }
  ]
}
```

**Respuesta:**
```json
{
  "id": 1,
  "fecha": "2026-06-18T12:00:00Z",
  "estado": "Pendiente",
  "total": 850,
  "lineas": [
    { "productoId": 1, "cantidad": 2 },
    { "productoId": 2, "cantidad": 1 }
  ]
}
```

### 3. Registrar pago
```http
POST /api/pagos
Content-Type: application/json

{
  "pedidoId": 1,
  "metodoPago": "Efectivo",
  "monto": 850
}
```

**Respuesta:**
```json
{ "mensaje": "Pago registrado" }
```

### 4. Consultar estado del pedido (CU4)
```http
GET /api/pedidos/1/estado
```

**Respuesta:**
```json
{
  "id": 1,
  "fecha": "2026-06-18T12:00:00Z",
  "estado": "EnPreparacion",
  "total": 850,
  "lineas": [
    { "productoId": 1, "cantidad": 2 },
    { "productoId": 2, "cantidad": 1 }
  ]
}
```

### 5. Actualizar estado del pedido
```http
PATCH /api/pedidos/1/estado
Content-Type: application/json

"Listo"
```

### 6. Obtener pedidos pendientes
```http
GET /api/pedidos/pendientes
```

## Patrones de diseño implementados

- **Repository Pattern** — Desacoplamiento de la persistencia
- **Service Pattern** — Lógica de negocio centralizada
- **DTO Pattern** — Separación entre API y dominio
- **Adapter Pattern** — Conversión de entidades del dominio a DTOs (`MapToResponse`)
- **Dependency Injection** — Inyección de dependencias en Program.cs
- **Singleton Pattern** — `IPedidoRepository` registrado como Singleton para compartir instancia única durante toda la ejecución, permitiendo persistencia de datos en memoria entre peticiones
- **Strategy Pattern** — Extensible para diferentes métodos de pago (implementable en `IPagoService`)
- **SOLID Principles** — Respeto de principios de diseño

## Estados de pedido

- `Pendiente` — Pedido recién creado
- `EnPreparacion` — Se ha registrado el pago
- `Listo` — Pedido listo para entregar
- `Entregado` — Pedido entregado
- `Rechazado` — Pedido rechazado

## Métodos de pago

- `Efectivo`
- `Debito`
- `Credito`
- `MercadoPago`


## Notas de desarrollo

- El repositorio in-memory persiste datos solo durante la ejecución
- Los datos de productos están hardcodeados, considerar cargarlos desde una fuente externa
