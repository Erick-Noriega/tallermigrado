using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface ITallesRepository
    {
        Task<List<Talles>> ListarTalles();
        Task CrearTalle(Talles talle);
        Task EliminarTalle(int id);
        Task<Talles> ObtenerTalle(int id);
        Task<bool> ExisteTalle(string descripcion, int? idExcluir = null);
        Task guardarCambios();
    }
}
