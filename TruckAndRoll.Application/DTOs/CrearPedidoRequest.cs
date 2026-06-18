namespace TruckAndRoll.Application.DTOs;

public record CrearPedidoRequest(
    List<LineaPedidoDto> Lineas
);
