using TruckAndRoll.Application.DTOs;
using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.Application.Interfaces;

public interface IPedidoService
{
    Task<PedidoResponse> CrearPedidoAsync(CrearPedidoRequest request);
    Task<PedidoResponse?> ConsultarEstadoAsync(int pedidoId);
    Task<PedidoResponse> ActualizarEstadoAsync(int pedidoId, EstadoPedido nuevoEstado);
    Task<IEnumerable<PedidoResponse>> ObtenerPendientesAsync();
}
