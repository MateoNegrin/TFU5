using Microsoft.AspNetCore.Mvc;
using TruckAndRoll.Application.DTOs;
using TruckAndRoll.Application.Interfaces;
using TruckAndRoll.Domain.Enums;

namespace TruckAndRoll.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _service;
    
    public PedidosController(IPedidoService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearPedidoRequest request)
    {
        try
        {
            var result = await _service.CrearPedidoAsync(request);
            return CreatedAtAction(nameof(ConsultarEstado), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
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

    [HttpGet("{id}/estado")]
    public async Task<IActionResult> ConsultarEstado(int id)
    {
        try
        {
            var result = await _service.ConsultarEstadoAsync(id);
            return result is null ? NotFound(new { error = $"Pedido {id} no encontrado" }) : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Error interno del servidor", details = ex.Message });
        }
    }

    [HttpPatch("{id}/estado")]
    public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoPedido nuevoEstado)
    {
        try
        {
            var result = await _service.ActualizarEstadoAsync(id, nuevoEstado);
            return Ok(result);
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

    [HttpGet("pendientes")]
    public async Task<IActionResult> Pendientes()
        => Ok(await _service.ObtenerPendientesAsync());
}
