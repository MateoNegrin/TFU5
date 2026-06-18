using TruckAndRoll.Domain.Entities;

namespace TruckAndRoll.Domain.Interfaces;

public interface IPedidoRepository
{
    Task<Pedido> GuardarPedidoAsync(Pedido pedido);
    Task<Pedido?> ConsultarPedidoAsync(int id);
    Task<Pedido> ActualizarPedidoAsync(Pedido pedido);
    Task<IEnumerable<Pedido>> ObtenerPendientesAsync();
}
