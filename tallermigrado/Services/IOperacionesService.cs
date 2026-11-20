using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IOperacionesService
    {
        Task<List<Operaciones>> ListarOperaciones();
        Task CrearOperacion(CrearOperacionDto operacion);
        Task EditarOperacion(EditarOperacionDto operacion);
        Task EliminarOperacion(int id);
    }
}
