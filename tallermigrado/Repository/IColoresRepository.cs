using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IColoresRepository
    {
        Task<List<Colores>> ListarColores();
        Task CrearColor(Colores color);
        Task EliminarColor(int id);
        Task<Colores> ObtenerColor(int id);
        Task<bool> ExisteColor(string nombre, int? idExcluir = null);
        Task guardarCambios();
    }
}
