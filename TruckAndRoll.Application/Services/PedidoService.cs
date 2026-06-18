using TruckAndRoll.Application.DTOs;
using TruckAndRoll.Application.Interfaces;
using TruckAndRoll.Domain.Entities;
using TruckAndRoll.Domain.Enums;
using TruckAndRoll.Domain.Interfaces;

namespace TruckAndRoll.Application.Services;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repo;
    
    public PedidoService(IPedidoRepository repo) => _repo = repo;

    public async Task<PedidoResponse> CrearPedidoAsync(CrearPedidoRequest request)
    {
        if (request.Lineas == null || request.Lineas.Count == 0)
            throw new ArgumentException("El pedido debe tener al menos una línea");

        var pedido = new Pedido { Fecha = DateTime.UtcNow, Estado = EstadoPedido.Pendiente };

        foreach (var linea in request.Lineas)
        {
            var producto = ObtenerProducto(linea.ProductoId)
                ?? throw new KeyNotFoundException($"Producto {linea.ProductoId} no encontrado");
            
            if (linea.Cantidad <= 0)
                throw new ArgumentException($"Cantidad debe ser mayor a 0 para producto {linea.ProductoId}");
                
            pedido.AgregarItem(producto, linea.Cantidad);
        }

        pedido.CalcularTotal();
        var guardado = await _repo.GuardarPedidoAsync(pedido);
        return MapToResponse(guardado);
    }

    public async Task<PedidoResponse?> ConsultarEstadoAsync(int pedidoId)
    {
        var pedido = await _repo.ConsultarPedidoAsync(pedidoId);
        return pedido is null ? null : MapToResponse(pedido);
    }

    public async Task<PedidoResponse> ActualizarEstadoAsync(int pedidoId, EstadoPedido nuevoEstado)
    {
        var pedido = await _repo.ConsultarPedidoAsync(pedidoId)
            ?? throw new KeyNotFoundException($"Pedido {pedidoId} no encontrado");
        pedido.Estado = nuevoEstado;
        var actualizado = await _repo.ActualizarPedidoAsync(pedido);
        return MapToResponse(actualizado);
    }

    public async Task<IEnumerable<PedidoResponse>> ObtenerPendientesAsync()
    {
        var pedidos = await _repo.ObtenerPendientesAsync();
        return pedidos.Select(MapToResponse);
    }

    private static PedidoResponse MapToResponse(Pedido p) => new(
        p.Id, p.Fecha, p.Estado, p.Total,
        p.Lineas.Select(l => new LineaPedidoDto(l.ProductoId, l.Cantidad)).ToList()
    );

    private static Producto? ObtenerProducto(int id)
    {
        var catalogo = new List<Producto>
        {
            new() { Id = 1, Nombre = "Hamburguesa", Precio = 350, Descripcion = "Hamburguesa clásica" },
            new() { Id = 2, Nombre = "Papas fritas", Precio = 150, Descripcion = "Papas fritas crujientes" },
            new() { Id = 3, Nombre = "Bebida",       Precio = 100, Descripcion = "Bebida fría" }
        };
        return catalogo.FirstOrDefault(p => p.Id == id);
    }
}
