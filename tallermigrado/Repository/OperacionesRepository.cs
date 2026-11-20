using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class OperacionesRepository : IOperacionesRepository
    {
        private readonly TallerDbContext _context;
        public OperacionesRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task CrearOperacion(Operaciones operacion)
        {
            _context.operaciones.Add(operacion);
            await _context.SaveChangesAsync();
        }

        public async Task<Operaciones> ObtenerOperacion(int id)
        {
            return await _context.operaciones.FindAsync(id);
        }

        public async Task<bool> ExisteOperacion(string nombre, int? idExcluir = null)
        {
            return await _context.operaciones.AnyAsync(o => o.NombreOperacion == nombre && (idExcluir == null || o.Id != idExcluir));
        }

        public async Task EliminarOperacion(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Operaciones>> ListarOperaciones()
        {
            return await _context.operaciones.ToListAsync();
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
