using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class OperacionesEmpresaService : IOperacionesEmpresaService
    {
        private readonly IOperacionesEmpresaRepository operacionesEmpresaRepository;
        public OperacionesEmpresaService(IOperacionesEmpresaRepository operacionesEmpresaRepository)
        {
            this.operacionesEmpresaRepository = operacionesEmpresaRepository;
        }

        public async Task<List<OperacionesEmpresa>> ListarOperacionesEmpresa()
        {
            return await operacionesEmpresaRepository.ListarOperacionesEmpresa();
        }

        public async Task CrearOperacionEmpresa(CrearOperacionEmpresaDto operacionEmpresa)
        {
            if (operacionEmpresa == null)
            {
                throw new ArgumentNullException(nameof(operacionEmpresa), "El objeto operación por empresa no puede ser nulo.");
            }

            if (await operacionesEmpresaRepository.ExisteOperacionEmpresa(operacionEmpresa.OperacionesId, operacionEmpresa.EmpresasId, operacionEmpresa.PrendasId))
            {
                throw new InvalidOperationException("Ya existe esa operación para la empresa y la prenda indicadas.");
            }

            var nuevo = new OperacionesEmpresa
            {
                OperacionesId = operacionEmpresa.OperacionesId,
                EmpresasId = operacionEmpresa.EmpresasId,
                prendasId = operacionEmpresa.PrendasId,
                precio = operacionEmpresa.Precio
            };
            await operacionesEmpresaRepository.CrearOperacionEmpresa(nuevo);
        }
    }
}
