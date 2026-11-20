using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class OperacionesService : IOperacionesService
    {
        private readonly IOperacionesRepository operacionesRepository;
        public OperacionesService(IOperacionesRepository operacionesRepository)
        {
            this.operacionesRepository = operacionesRepository;
        }
        public async Task CrearOperacion(CrearOperacionDto operacion)
        {
            if (operacion != null)
            {
                if (await operacionesRepository.ExisteOperacion(operacion.NombreOperacion))
                {
                    throw new InvalidOperationException("Ya existe una operación con ese nombre.");
                }
                var nuevaOperacion = new Operaciones
                {
                    NombreOperacion = operacion.NombreOperacion,
                    prendasId = operacion.PrendasId
                };
                await operacionesRepository.CrearOperacion(nuevaOperacion);
            }
            else
            {
                throw new ArgumentNullException(nameof(operacion), "El objeto operación no puede ser nulo.");
            }
        }

        public async Task EditarOperacion(EditarOperacionDto operacion)
        {
            var operacionExistente = await operacionesRepository.ObtenerOperacion(operacion.Id)
                ?? throw new ArgumentException($"No se encontró una operación con el ID {operacion.Id}");
            if (await operacionesRepository.ExisteOperacion(operacion.NombreOperacion, operacion.Id))
            {
                throw new InvalidOperationException("Ya existe otra operación con ese nombre.");
            }
            operacionExistente.NombreOperacion = operacion.NombreOperacion;
            await operacionesRepository.guardarCambios();
        }

        public Task EliminarOperacion(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Operaciones>> ListarOperaciones()
        {
            var operaciones = operacionesRepository.ListarOperaciones();
            if (operaciones != null)
            {
                return operaciones;
            }
            else
            {
                throw new ArgumentException("No se encontraron operaciones.");
            }
        }
    }
}
