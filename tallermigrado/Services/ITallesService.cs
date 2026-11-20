using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface ITallesService
    {
        Task<List<Talles>> ListarTalles();
        Task CrearTalle(CrearTalleDto talle);
        Task EditarTalle(EditarTalleDto talle);
        Task EliminarTalle(int id);
    }
}
