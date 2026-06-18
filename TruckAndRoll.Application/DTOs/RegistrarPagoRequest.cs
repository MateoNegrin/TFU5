using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.Application.DTOs;

public record RegistrarPagoRequest(
    int PedidoId,
    MetodoPago MetodoPago,
    decimal Monto
);
