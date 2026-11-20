using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IPagosEmpleadoRepository
    {
        Task GuardarPago(PagosEmpleado pago);
        Task<PagosEmpleado?> ObtenerPagoPorMes(string empleadoId, string mes);
        Task<List<PagosEmpleado>> ObtenerPagosPorEmpleado(string? empleadoId, string? mes);
        Task<PagosEmpleado?> ObtenerPagoPorId(int id);
        Task MarcarComoPagado(PagosEmpleado pago);
    }
}
