using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class ColoresService : IColoresService
    {
        private readonly IColoresRepository coloresRepository;
        public ColoresService(IColoresRepository coloresRepository)
        {
            this.coloresRepository = coloresRepository;
        }
        public async Task CrearColor(CrearColorDto color)
        {
            if (color != null)
            {
                if (await coloresRepository.ExisteColor(color.Nombre))
                {
                    throw new InvalidOperationException("Ya existe un color con ese nombre.");
                }
                var nuevoColor = new Colores
                {
                    Nombre = color.Nombre
                };
                await coloresRepository.CrearColor(nuevoColor);
            }
            else
            {
                throw new ArgumentNullException(nameof(color), "El objeto color no puede ser nulo.");
            }
        }

        public async Task EditarColor(EditarColorDto color)
        {
            var colorExistente = await coloresRepository.ObtenerColor(color.Id)
                ?? throw new ArgumentException($"No se encontró un color con el ID {color.Id}");
            if (await coloresRepository.ExisteColor(color.Nombre, color.Id))
            {
                throw new InvalidOperationException("Ya existe otro color con ese nombre.");
            }
            colorExistente.Nombre = color.Nombre;
            await coloresRepository.guardarCambios();
        }

        public Task EliminarColor(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Colores>> ListarColores()
        {
            var colores = coloresRepository.ListarColores();
            if (colores != null)
            {
                return colores;
            }
            else
            {
                throw new ArgumentException("No se encontraron colores.");
            }
        }
    }
}
