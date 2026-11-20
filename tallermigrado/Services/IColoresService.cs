using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IColoresService
    {
        Task<List<Colores>> ListarColores();
        Task CrearColor(CrearColorDto color);
        Task EditarColor(EditarColorDto color);
        Task EliminarColor(int id);
    }
}
