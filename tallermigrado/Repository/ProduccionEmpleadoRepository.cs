using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class ProduccionEmpleadoRepository : IProduccionEmpleadoRepository
    {
        private readonly TallerDbContext _context;

        public ProduccionEmpleadoRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prendas>> ObtenerPrendas()
        {
            return await _context.prendas
                .Where(p => _context.fichas.Any(f => f.prendasID == p.Id))
                .ToListAsync();
        }

        public async Task<List<Empresas>> ObtenerEmpresasPorPrenda(int prendaId)
        {
            return await _context.fichas
                .Where(f => f.prendasID == prendaId)
                .Select(f => f.Empresas!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Colores>> ObtenerColoresPorFicha(int prendaId, int empresaId)
        {
            return await _context.fichas
                .Where(f => f.prendasID == prendaId && f.EmpresasId == empresaId)
                .Select(f => f.Color!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Talles>> ObtenerTallesPorFicha(int prendaId, int empresaId, int colorId)
        {
            return await _context.fichas
                .Where(f => f.prendasID == prendaId && f.EmpresasId == empresaId && f.colorId == colorId)
                .Select(f => f.talle!)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<string>> ObtenerCortesPorFicha(int prendaId, int empresaId, int colorId, int talleId)
        {
            return await _context.fichas
                .Where(f => f.prendasID == prendaId && f.EmpresasId == empresaId && f.colorId == colorId && f.talleId == talleId)
                .Select(f => f.NumerodeCorte)
                .Distinct()
                .ToListAsync();
        }

        public async Task<List<Operaciones>> ObtenerOperacionesPorFicha(int prendaId, int empresaId)
        {
            return await _context.operaciones
                .Where(o => o.prendasId == prendaId)
                .ToListAsync();
        }

        public async Task<Ficha?> ObtenerFichaExacta(int prendaId, int empresaId, int colorId, int talleId, string corte)
        {
            return await _context.fichas.FirstOrDefaultAsync(f =>
                f.prendasID == prendaId &&
                f.EmpresasId == empresaId &&
                f.colorId == colorId &&
                f.talleId == talleId &&
                f.NumerodeCorte == corte);
        }

        public async Task<OperacionesEmpresa?> ObtenerOperacionEmpresa(int operacionId, int empresaId, int prendaId)
        {
            return await _context.operacionesempresa.FirstOrDefaultAsync(oe =>
                oe.OperacionesId == operacionId &&
                oe.EmpresasId == empresaId &&
                oe.prendasId == prendaId);
        }

        public async Task<bool> ExisteProduccion(int operacionId, int colorId, int talleId, string corte)
        {
            return await _context.produempleado.AnyAsync(p =>
                p.operacionesId == operacionId &&
                p.coloresId == colorId &&
                p.talleId == talleId &&
                p.corte == corte);
        }

        public async Task CrearProduccion(ProduccionEmpleados produccion)
        {
            _context.produempleado.Add(produccion);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Empleados>> ObtenerEmpleados()
        {
            return await _context.Users
                .OfType<Empleados>()
                .OrderBy(e => e.nombre)
                .ToListAsync();
        }

        public async Task<List<ProduccionEmpleados>> ObtenerProduccionPorEmpleado()
        {
            return await _context.produempleado
                .Where(p => p.pagosEmpleadoId == null)
                .Include(p => p.empleados)
                .Include(p => p.Operaciones)
                .Include(p => p.color)
                .Include(p => p.talle)
                .Include(p => p.Empresas)
                .Include(p => p.Prendas)
                .OrderBy(p => p.empleadoId)
                .ThenByDescending(p => p.Fecha)
                .ToListAsync();
        }

        public async Task<List<ProduccionEmpleados>> ObtenerProduccionPorIds(List<int> ids)
        {
            return await _context.produempleado
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}
