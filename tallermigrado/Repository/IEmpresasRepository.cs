using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IEmpresasRepository
    {
        Task<List<Empresas>> ListarEmpresas();
        Task CrearEmpresa(Empresas empresa);
        Task EliminarEmpresa(int id);
        Task<Empresas> ObtenerEmpresa(int id);
        Task<bool> ExisteEmpresa(string nombre, int? idExcluir = null);
        Task guardarCambios();
    }
}
