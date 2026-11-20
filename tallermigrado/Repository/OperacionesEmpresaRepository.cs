using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class OperacionesEmpresaRepository : IOperacionesEmpresaRepository
    {
        private readonly TallerDbContext _context;
        public OperacionesEmpresaRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task<List<OperacionesEmpresa>> ListarOperacionesEmpresa()
        {
            return await _context.operacionesempresa
                .Include(oe => oe.Operaciones)
                .Include(oe => oe.Empresas)
                .Include(oe => oe.prendas)
                .ToListAsync();
        }

        public async Task<OperacionesEmpresa?> ObtenerOperacionEmpresa(int id)
        {
            return await _context.operacionesempresa.FindAsync(id);
        }

        public async Task<bool> ExisteOperacionEmpresa(int operacionesId, int empresasId, int prendasId, int? idExcluir = null)
        {
            return await _context.operacionesempresa.AnyAsync(oe =>
                oe.OperacionesId == operacionesId &&
                oe.EmpresasId == empresasId &&
                oe.prendasId == prendasId &&
                (idExcluir == null || oe.Id != idExcluir));
        }

        public async Task CrearOperacionEmpresa(OperacionesEmpresa operacionEmpresa)
        {
            _context.operacionesempresa.Add(operacionEmpresa);
            await _context.SaveChangesAsync();

            await _context.produempleado
                .Where(p => p.operacionesId == operacionEmpresa.OperacionesId
                    && p.empresasId == operacionEmpresa.EmpresasId
                    && p.prendasId == operacionEmpresa.prendasId
                    && p.precio == null)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.precio, operacionEmpresa.precio));
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
