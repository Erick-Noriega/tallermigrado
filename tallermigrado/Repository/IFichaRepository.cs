using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IFichaRepository
    {
        Task<List<Ficha>> ListarFichas();
        Task CrearFicha(Ficha ficha);
        Task<Ficha?> ObtenerFicha(int id);
        Task<bool> ExisteFicha(int prendasId, int talleId, int colorId, int empresasId, string numeroDeCorte, int? idExcluir = null);
        Task guardarCambios();
    }
}
