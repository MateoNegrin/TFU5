using TruckAndRoll.Domain.Entities;
using TruckAndRoll.Domain.Enums;
using TruckAndRoll.Domain.Interfaces;

namespace TruckAndRoll.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly List<Pedido> _db = new();
    private int _nextId = 1;

    public Task<Pedido> GuardarPedidoAsync(Pedido pedido)
    {
        pedido.Id = _nextId++;
        _db.Add(pedido);
        return Task.FromResult(pedido);
    }

    public Task<Pedido?> ConsultarPedidoAsync(int id)
        => Task.FromResult(_db.FirstOrDefault(p => p.Id == id));

    public Task<Pedido> ActualizarPedidoAsync(Pedido pedido)
    {
        var idx = _db.FindIndex(p => p.Id == pedido.Id);
        _db[idx] = pedido;
        return Task.FromResult(pedido);
    }

    public Task<IEnumerable<Pedido>> ObtenerPendientesAsync()
        => Task.FromResult(_db.Where(p => p.Estado == EstadoPedido.Pendiente));
}
