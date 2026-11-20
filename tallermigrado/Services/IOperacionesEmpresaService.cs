using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IOperacionesEmpresaService
    {
        Task<List<OperacionesEmpresa>> ListarOperacionesEmpresa();
        Task CrearOperacionEmpresa(CrearOperacionEmpresaDto operacionEmpresa);
    }
}
