using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IOperacionesEmpresaRepository
    {
        Task<List<OperacionesEmpresa>> ListarOperacionesEmpresa();
        Task<OperacionesEmpresa?> ObtenerOperacionEmpresa(int id);
        Task<bool> ExisteOperacionEmpresa(int operacionesId, int empresasId, int prendasId, int? idExcluir = null);
        Task CrearOperacionEmpresa(OperacionesEmpresa operacionEmpresa);
        Task guardarCambios();
    }
}
