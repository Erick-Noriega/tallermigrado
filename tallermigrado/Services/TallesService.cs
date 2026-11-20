using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class TallesService : ITallesService
    {
        private readonly ITallesRepository tallesRepository;
        public TallesService(ITallesRepository tallesRepository)
        {
            this.tallesRepository = tallesRepository;
        }
        public async Task CrearTalle(CrearTalleDto talle)
        {
            if (talle != null)
            {
                if (await tallesRepository.ExisteTalle(talle.DescripcionTalle))
                {
                    throw new InvalidOperationException("Ya existe un talle con esa descripción.");
                }
                var nuevoTalle = new Talles
                {
                    DescripcionTalle = talle.DescripcionTalle
                };
                await tallesRepository.CrearTalle(nuevoTalle);
            }
            else
            {
                throw new ArgumentNullException(nameof(talle), "El objeto talle no puede ser nulo.");
            }
        }

        public async Task EditarTalle(EditarTalleDto talle)
        {
            var talleExistente = await tallesRepository.ObtenerTalle(talle.Id)
                ?? throw new ArgumentException($"No se encontró un talle con el ID {talle.Id}");
            if (await tallesRepository.ExisteTalle(talle.DescripcionTalle, talle.Id))
            {
                throw new InvalidOperationException("Ya existe otro talle con esa descripción.");
            }
            talleExistente.DescripcionTalle = talle.DescripcionTalle;
            await tallesRepository.guardarCambios();
        }

        public Task EliminarTalle(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Talles>> ListarTalles()
        {
            var talles = tallesRepository.ListarTalles();
            if (talles != null)
            {
                return talles;
            }
            else
            {
                throw new ArgumentException("No se encontraron talles.");
            }
        }
    }
}
