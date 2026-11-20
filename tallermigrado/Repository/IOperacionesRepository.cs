using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IOperacionesRepository
    {
        Task<List<Operaciones>> ListarOperaciones();
        Task CrearOperacion(Operaciones operacion);
        Task EliminarOperacion(int id);
        Task<Operaciones> ObtenerOperacion(int id);
        Task<bool> ExisteOperacion(string nombre, int? idExcluir = null);
        Task guardarCambios();
    }
}
