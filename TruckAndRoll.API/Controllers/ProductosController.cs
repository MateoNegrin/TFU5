using Microsoft.AspNetCore.Mvc;
using TruckAndRoll.Domain.Entities;

namespace TruckAndRoll.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    [HttpGet]
    public IActionResult ListarProductos()
    {
        var catalogo = new List<Producto>
        {
            new() { Id = 1, Nombre = "Hamburguesa", Precio = 350, Descripcion = "Hamburguesa clásica" },
            new() { Id = 2, Nombre = "Papas fritas", Precio = 150, Descripcion = "Papas fritas crujientes" },
            new() { Id = 3, Nombre = "Bebida",       Precio = 100, Descripcion = "Bebida fría" }
        };
        return Ok(catalogo);
    }
}
