using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IPagosEmpleadoService
    {
        Task<PagosEmpleado> CrearPago(CrearPagoEmpleadoDto dto, string empleadoId);
        Task<List<PagosEmpleado>> ObtenerPagosPorEmpleado(string? empleadoId, string? mes);
        Task<PagosEmpleado?> ObtenerPagoPorId(int id);
        Task MarcarComoPagado(int id);
    }
}
