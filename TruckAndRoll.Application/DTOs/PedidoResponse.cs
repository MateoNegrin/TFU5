using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.Application.DTOs;

public record PedidoResponse(
    int Id,
    DateTime Fecha,
    EstadoPedido Estado,
    decimal Total,
    List<LineaPedidoDto> Lineas
);
