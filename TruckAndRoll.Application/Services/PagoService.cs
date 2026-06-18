using TruckAndRoll.Application.DTOs;
using TruckAndRoll.Application.Interfaces;
using TruckAndRoll.Domain.Entities;
using TruckAndRoll.Domain.Enums;
using TruckAndRoll.Domain.Interfaces;

namespace TruckAndRoll.Application.Services;

public class PagoService : IPagoService
{
    private readonly IPedidoRepository _repo;

    public PagoService(IPedidoRepository repo) => _repo = repo;

    public async Task<bool> RegistrarPagoAsync(RegistrarPagoRequest request)
    {
        var pedido = await _repo.ConsultarPedidoAsync(request.PedidoId)
            ?? throw new KeyNotFoundException();

        pedido.Pago = new Pago
        {
            PedidoId = request.PedidoId,
            Monto = request.Monto,
            MetodoPago = request.MetodoPago,
            Estado = EstadoPago.Aprobado
        };
        pedido.Estado = EstadoPedido.EnPreparacion;
        await _repo.ActualizarPedidoAsync(pedido);
        return true;
    }
}
