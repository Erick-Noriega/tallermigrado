using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class ColoresRepository : IColoresRepository
    {
        private readonly TallerDbContext _context;
        public ColoresRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task CrearColor(Colores color)
        {
            _context.colores.Add(color);
            await _context.SaveChangesAsync();
        }

        public async Task<Colores> ObtenerColor(int id)
        {
            return await _context.colores.FindAsync(id);
        }

        public async Task<bool> ExisteColor(string nombre, int? idExcluir = null)
        {
            return await _context.colores.AnyAsync(c => c.Nombre == nombre && (idExcluir == null || c.Id != idExcluir));
        }

        public async Task EliminarColor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Colores>> ListarColores()
        {
            return await _context.colores.ToListAsync();
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
