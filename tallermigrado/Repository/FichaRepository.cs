using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class FichaRepository : IFichaRepository
    {
        private readonly TallerDbContext _context;
        public FichaRepository(TallerDbContext context)
        {
            _context = context;
        }

        public async Task CrearFicha(Ficha ficha)
        {
            _context.fichas.Add(ficha);
            await _context.SaveChangesAsync();
        }

        public async Task<Ficha?> ObtenerFicha(int id)
        {
            return await _context.fichas.FindAsync(id);
        }

        public async Task<bool> ExisteFicha(int prendasId, int talleId, int colorId, int empresasId, string numeroDeCorte, int? idExcluir = null)
        {
            return await _context.fichas.AnyAsync(f =>
                f.prendasID == prendasId &&
                f.talleId == talleId &&
                f.colorId == colorId &&
                f.EmpresasId == empresasId &&
                f.NumerodeCorte == numeroDeCorte &&
                (idExcluir == null || f.Id != idExcluir));
        }

        public async Task<List<Ficha>> ListarFichas()
        {
            return await _context.fichas
                .Include(f => f.prendas)
                .Include(f => f.talle)
                .Include(f => f.Color)
                .Include(f => f.Empresas)
                .ToListAsync();
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
