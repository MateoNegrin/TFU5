# Ejemplos de Uso de la API Truck & Roll

Este archivo contiene ejemplos de cómo usar la API con curl. Asegúrate de que la API está ejecutándose en `http://localhost:5042`

> ⚠️ **Nota importante:** Los ejemplos están escritos para **PowerShell (Windows)**. Si usas bash/Linux, puedes reemplazar los backticks `` ` `` por barras invertidas `\` y las comillas escapadas `\"` por comillas simples `'`.

## 1. Listar productos (menú)

```powershell
curl -X GET "http://localhost:5042/api/productos" `
  -H "accept: application/json"
```

**Respuesta esperada:**
```json
[
  {
    "id": 1,
    "nombre": "Hamburguesa",
    "precio": 350,
    "descripcion": "Hamburguesa clásica"
  },
  {
    "id": 2,
    "nombre": "Papas fritas",
    "precio": 150,
    "descripcion": "Papas fritas crujientes"
  },
  {
    "id": 3,
    "nombre": "Bebida",
    "precio": 100,
    "descripcion": "Bebida fría"
  }
]
```

## 2. Crear pedido (CU1)

```powershell
curl -X POST "http://localhost:5042/api/pedidos" `
  -H "Content-Type: application/json" `
  -d "{
    `"lineas`": [
      { `"productoId`": 1, `"cantidad`": 2 },
      { `"productoId`": 2, `"cantidad`": 1 }
    ]
  }"
```

**Respuesta esperada (Status 201 Created):**
```json
{
  "id": 1,
  "fecha": "2026-06-18T12:00:00Z",
  "estado": "Pendiente",
  "total": 850,
  "lineas": [
    {
      "productoId": 1,
      "cantidad": 2
    },
    {
      "productoId": 2,
      "cantidad": 1
    }
  ]
}
```

> **Nota:** El ID del pedido creado se usa en las siguientes llamadas

## 3. Registrar pago

```powershell
curl -X POST "http://localhost:5042/api/pagos" `
  -H "Content-Type: application/json" `
  -d "{
    `"pedidoId`": 1,
    `"metodoPago`": `"Efectivo`",
    `"monto`": 850
  }"
```

**Respuesta esperada (Status 200 OK):**
```json
{
  "mensaje": "Pago registrado"
}
```

> **Nota:** `metodoPago` puede ser: `Efectivo`, `Debito`, `Credito`, `MercadoPago`

## 4. Consultar estado del pedido (CU4)

```powershell
curl -X GET "http://localhost:5042/api/pedidos/1/estado" `
  -H "accept: application/json"
```

**Respuesta esperada:**
```json
{
  "id": 1,
  "fecha": "2026-06-18T12:00:00Z",
  "estado": "EnPreparacion",
  "total": 850,
  "lineas": [
    {
      "productoId": 1,
      "cantidad": 2
    },
    {
      "productoId": 2,
      "cantidad": 1
    }
  ]
}
```

> **Nota:** El estado cambió a `EnPreparacion` después de registrar el pago

## 5. Actualizar estado del pedido

```powershell
curl -X PATCH "http://localhost:5042/api/pedidos/1/estado" `
  -H "Content-Type: application/json" `
  -d "`"Listo`""
```

**Respuesta esperada:**
```json
{
  "id": 1,
  "fecha": "2026-06-18T12:00:00Z",
  "estado": "Listo",
  "total": 850,
  "lineas": [
    {
      "productoId": 1,
      "cantidad": 2
    },
    {
      "productoId": 2,
      "cantidad": 1
    }
  ]
}
```

> **Estados válidos:** `Pendiente`, `EnPreparacion`, `Listo`, `Entregado`, `Rechazado`

## 6. Obtener pedidos pendientes

```powershell
curl -X GET "http://localhost:5042/api/pedidos/pendientes" `
  -H "accept: application/json"
```

**Respuesta esperada:**
```json
[
  {
    "id": 2,
    "fecha": "2026-06-18T13:00:00Z",
    "estado": "Pendiente",
    "total": 500,
    "lineas": [
      {
        "productoId": 3,
        "cantidad": 5
      }
    ]
  }
]
```

## Flujo completo de ejemplo

```powershell
# 1. Ver el menú
curl -X GET "http://localhost:5042/api/productos"

# 2. Crear un nuevo pedido
curl -X POST "http://localhost:5042/api/pedidos" `
  -H "Content-Type: application/json" `
  -d "{`"lineas`": [{`"productoId`": 1, `"cantidad`": 3}]}"

# Guardar el ID del pedido retornado (ej: 1)

# 3. Registrar pago
curl -X POST "http://localhost:5042/api/pagos" `
  -H "Content-Type: application/json" `
  -d "{`"pedidoId`": 1, `"metodoPago`": `"Credito`", `"monto`": 1050}"

# 4. Consultar estado (debe ser "EnPreparacion")
curl -X GET "http://localhost:5042/api/pedidos/1/estado"

# 5. Actualizar a "Listo" (simular que la cocina preparó el pedido)
curl -X PATCH "http://localhost:5042/api/pedidos/1/estado" `
  -H "Content-Type: application/json" `
  -d "`"Listo`""

# 6. Consultar estado final
curl -X GET "http://localhost:5042/api/pedidos/1/estado"
```


## Swagger UI

También puedes probar la API usando Swagger UI:

```
http://localhost:5042/swagger/index.html
```

Ahí podrás ejecutar los endpoints directamente desde el navegador.

## Códigos de estado HTTP esperados

| Operación | Código | Significado |
|-----------|--------|-------------|
| Crear pedido | 201 | Pedido creado exitosamente |
| Registrar pago | 200 | Pago registrado |
| Consultar estado | 200 | Pedido encontrado |
| Consultar estado (no existe) | 404 | Pedido no encontrado |
| Actualizar estado | 200 | Estado actualizado |

## Notas de desarrollo

- Los pedidos están almacenados en memoria, se pierden cuando se reinicia la API
- Los IDs se asignan automáticamente comenzando en 1
