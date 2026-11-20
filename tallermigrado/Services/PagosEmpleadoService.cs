using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class PagosEmpleadoService : IPagosEmpleadoService
    {
        private readonly IPagosEmpleadoRepository _pagosRepository;
        private readonly IProduccionEmpleadoRepository _produccionRepository;

        public PagosEmpleadoService(IPagosEmpleadoRepository pagosRepository, IProduccionEmpleadoRepository produccionRepository)
        {
            _pagosRepository = pagosRepository;
            _produccionRepository = produccionRepository;
        }

        public async Task<PagosEmpleado> CrearPago(CrearPagoEmpleadoDto dto, string empleadoId)
        {
            if (string.IsNullOrEmpty(empleadoId))
            {
                throw new InvalidOperationException("No se pudo identificar el usuario de la sesión.");
            }

            var idsSeleccionados = dto.ProduccionIds.Distinct().ToList();
            if (idsSeleccionados.Count == 0)
            {
                throw new InvalidOperationException("Debe seleccionar al menos un registro de producción.");
            }

            var registros = await _produccionRepository.ObtenerProduccionPorIds(idsSeleccionados);
            if (registros.Count != idsSeleccionados.Count)
            {
                throw new InvalidOperationException("Alguno de los registros seleccionados no existe.");
            }

            if (registros.Any(r => r.precio == null))
            {
                throw new InvalidOperationException("Hay registros seleccionados sin precio asignado.");
            }

            var total = registros.Sum(r => r.CantidadDePrendas * r.precio!.Value);

            var pago = await _pagosRepository.ObtenerPagoPorMes(empleadoId, dto.Mes);
            if (pago == null)
            {
                pago = new PagosEmpleado
                {
                    Mes = dto.Mes,
                    Pagado = false,
                    total = 0,
                    empleadosId = empleadoId
                };
            }

            foreach (var registro in registros)
            {
                registro.PagoEmpleado = pago;
                pago.Registros.Add(registro);
            }

            pago.total = pago.Registros.Sum(r => r.CantidadDePrendas * r.precio!.Value);

            await _pagosRepository.GuardarPago(pago);
            return pago;
        }

        public Task<List<PagosEmpleado>> ObtenerPagosPorEmpleado(string? empleadoId, string? mes)
        {
            return _pagosRepository.ObtenerPagosPorEmpleado(empleadoId, mes);
        }

        public Task<PagosEmpleado?> ObtenerPagoPorId(int id)
        {
            return _pagosRepository.ObtenerPagoPorId(id);
        }

        public async Task MarcarComoPagado(int id)
        {
            var pago = await _pagosRepository.ObtenerPagoPorId(id)
                ?? throw new InvalidOperationException("El pago indicado no existe.");

            await _pagosRepository.MarcarComoPagado(pago);
        }
    }
}
