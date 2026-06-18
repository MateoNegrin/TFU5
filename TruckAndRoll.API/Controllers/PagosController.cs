using Microsoft.AspNetCore.Mvc;
using TruckAndRoll.Application.DTOs;
using TruckAndRoll.Application.Interfaces;

namespace TruckAndRoll.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PagosController : ControllerBase
{
    private readonly IPagoService _service;
    
    public PagosController(IPagoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Registrar([FromBody] RegistrarPagoRequest request)
    {
        try
        {
            var ok = await _service.RegistrarPagoAsync(request);
            return ok ? Ok(new { mensaje = "Pago registrado", pedidoId = request.PedidoId }) : BadRequest(new { error = "No se pudo registrar el pago" });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", details = ex.Message });
        }
    }
}
