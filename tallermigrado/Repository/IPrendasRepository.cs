using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IPrendasRepository
    {
        Task<List<Prendas>> ListarPrendas();
        Task CrearPrenda(Prendas prenda);
        Task EliminarPrenda(int id);
        Task<Prendas> ObtenerPrendas(int id);
        Task<bool> ExistePrenda(string modelo, int? idExcluir = null);
        Task guardarCambios();
    }
}
