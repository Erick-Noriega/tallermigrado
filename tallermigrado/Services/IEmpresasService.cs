using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IEmpresasService
    {
        Task<List<Empresas>> ListarEmpresas();
        Task CrearEmpresa(CrearEmpresaDto empresa);
        Task EditarEmpresa(EditarEmpresaDto empresa);
        Task EliminarEmpresa(int id);
    }
}
