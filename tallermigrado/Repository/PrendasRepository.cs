using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class PrendasRepository : IPrendasRepository
    {
        private readonly TallerDbContext _context;
        public PrendasRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task CrearPrenda(Prendas prenda)
        {
            _context.prendas.Add(prenda);
            await _context.SaveChangesAsync();
        }

        public async Task<Prendas> ObtenerPrendas(int id)
        {
            return await _context.prendas.FindAsync(id);
        }

        public async Task<bool> ExistePrenda(string modelo, int? idExcluir = null)
        {
            return await _context.prendas.AnyAsync(p => p.ModeloPrenda == modelo && (idExcluir == null || p.Id != idExcluir));
        }

        public async Task EliminarPrenda(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Prendas>> ListarPrendas()
        {
           var prendas = await _context.prendas.ToListAsync();
            return prendas;
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }       
    }
}
