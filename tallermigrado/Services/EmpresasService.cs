using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class EmpresasService : IEmpresasService
    {
        private readonly IEmpresasRepository empresasRepository;
        public EmpresasService(IEmpresasRepository empresasRepository)
        {
            this.empresasRepository = empresasRepository;
        }
        public async Task CrearEmpresa(CrearEmpresaDto empresa)
        {
            if (empresa != null)
            {
                if (await empresasRepository.ExisteEmpresa(empresa.NombreEmpresa))
                {
                    throw new InvalidOperationException("Ya existe una empresa con ese nombre.");
                }
                var nuevaEmpresa = new Empresas
                {
                    NombreEmpresa = empresa.NombreEmpresa
                };
                await empresasRepository.CrearEmpresa(nuevaEmpresa);
            }
            else
            {
                throw new ArgumentNullException(nameof(empresa), "El objeto empresa no puede ser nulo.");
            }
        }

        public async Task EditarEmpresa(EditarEmpresaDto empresa)
        {
            var empresaExistente = await empresasRepository.ObtenerEmpresa(empresa.Id)
                ?? throw new ArgumentException($"No se encontró una empresa con el ID {empresa.Id}");
            if (await empresasRepository.ExisteEmpresa(empresa.NombreEmpresa, empresa.Id))
            {
                throw new InvalidOperationException("Ya existe otra empresa con ese nombre.");
            }
            empresaExistente.NombreEmpresa = empresa.NombreEmpresa;
            await empresasRepository.guardarCambios();
        }

        public Task EliminarEmpresa(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Empresas>> ListarEmpresas()
        {
            var empresas = empresasRepository.ListarEmpresas();
            if (empresas != null)
            {
                return empresas;
            }
            else
            {
                throw new ArgumentException("No se encontraron empresas.");
            }
        }
    }
}
