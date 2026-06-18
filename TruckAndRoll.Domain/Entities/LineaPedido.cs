namespace TruckAndRoll.Domain.Entities;

public class LineaPedido
{
    public int Cantidad { get; set; }
    public decimal SubTotal { get; private set; }

    public int ProductoId { get; set; }
    public required Producto Producto { get; set; }

    public void CalcularSubTotal()
        => SubTotal = Cantidad * Producto.Precio;
}
