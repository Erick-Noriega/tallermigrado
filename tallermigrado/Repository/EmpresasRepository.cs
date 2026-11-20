using Microsoft.EntityFrameworkCore;
using tallermigrado.Data;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class EmpresasRepository : IEmpresasRepository
    {
        private readonly TallerDbContext _context;
        public EmpresasRepository(TallerDbContext context)
        {
            _context = context;
        }
        public async Task CrearEmpresa(Empresas empresa)
        {
            _context.empresas.Add(empresa);
            await _context.SaveChangesAsync();
        }

        public async Task<Empresas> ObtenerEmpresa(int id)
        {
            return await _context.empresas.FindAsync(id);
        }

        public async Task<bool> ExisteEmpresa(string nombre, int? idExcluir = null)
        {
            return await _context.empresas.AnyAsync(e => e.NombreEmpresa == nombre && (idExcluir == null || e.Id != idExcluir));
        }

        public async Task EliminarEmpresa(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Empresas>> ListarEmpresas()
        {
            return await _context.empresas.ToListAsync();
        }

        public async Task guardarCambios()
        {
            await _context.SaveChangesAsync();
        }
    }
}
