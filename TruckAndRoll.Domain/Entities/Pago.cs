using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.Domain.Entities;

public class Pago
{
    public int Id { get; set; }
    public decimal Monto { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public EstadoPago Estado { get; set; }
    public int PedidoId { get; set; }
}
