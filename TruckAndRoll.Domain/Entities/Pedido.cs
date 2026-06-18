using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.Domain.Entities;

public class Pedido
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public EstadoPedido Estado { get; set; }
    public decimal Total { get; private set; }
    public List<LineaPedido> Lineas { get; set; } = new();
    public Pago? Pago { get; set; }

    public void AgregarItem(Producto producto, int cantidad)
    {
        var linea = new LineaPedido { Producto = producto, ProductoId = producto.Id, Cantidad = cantidad };
        linea.CalcularSubTotal();
        Lineas.Add(linea);
    }

    public void CalcularTotal()
        => Total = Lineas.Sum(l => l.SubTotal);
}
