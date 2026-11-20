using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class PagosEmpleadoRepository : IPagosEmpleadoRepository
    {
        private readonly TallerDbContext _context;

        public PagosEmpleadoRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task GuardarPago(PagosEmpleado pago)
        {
            if (pago.Id == 0)
            {
                _context.pagosempleado.Add(pago);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<PagosEmpleado?> ObtenerPagoPorMes(string empleadoId, string mes)
        {
            return await _context.pagosempleado
                .Include(p => p.Registros)
                .FirstOrDefaultAsync(p => p.empleadosId == empleadoId && p.Mes == mes);
        }

        public async Task<List<PagosEmpleado>> ObtenerPagosPorEmpleado(string? empleadoId, string? mes)
        {
            var consulta = _context.pagosempleado
                .Include(p => p.empleados)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Operaciones)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Prendas)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Empresas)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.color)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.talle)
                .AsQueryable();

            if (!string.IsNullOrEmpty(empleadoId))
            {
                consulta = consulta.Where(p => p.empleadosId == empleadoId);
            }

            if (!string.IsNullOrEmpty(mes))
            {
                consulta = consulta.Where(p => p.Mes == mes);
            }

            return await consulta
                .OrderBy(p => p.Mes)
                .ToListAsync();
        }

        public async Task MarcarComoPagado(PagosEmpleado pago)
        {
            pago.Pagado = true;
            await _context.SaveChangesAsync();
        }

        public async Task<PagosEmpleado?> ObtenerPagoPorId(int id)
        {
            return await _context.pagosempleado
                .Include(p => p.empleados)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Operaciones)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Prendas)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.Empresas)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.color)
                .Include(p => p.Registros)
                    .ThenInclude(r => r.talle)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
