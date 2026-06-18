using TruckAndRoll.Application.DTOs;

namespace TruckAndRoll.Application.Interfaces;

public interface IPagoService
{
    Task<bool> RegistrarPagoAsync(RegistrarPagoRequest request);
}
