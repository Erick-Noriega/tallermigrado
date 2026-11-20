using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class TallesRepository : ITallesRepository
    {
        private readonly TallerDbContext _context;
        public TallesRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task CrearTalle(Talles talle)
        {
            _context.talles.Add(talle);
            await _context.SaveChangesAsync();
        }

        public async Task<Talles> ObtenerTalle(int id)
        {
            return await _context.talles.FindAsync(id);
        }

        public async Task<bool> ExisteTalle(string descripcion, int? idExcluir = null)
        {
            return await _context.talles.AnyAsync(t => t.DescripcionTalle == descripcion && (idExcluir == null || t.Id != idExcluir));
        }

        public async Task EliminarTalle(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Talles>> ListarTalles()
        {
            return await _context.talles.ToListAsync();
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
