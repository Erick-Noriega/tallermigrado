using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class Prendas_Service : IprendasService
    {
        private readonly IPrendasRepository prendasRepository;
        public Prendas_Service(IPrendasRepository prendasRepository)
        {
            this.prendasRepository = prendasRepository;
        }
        public async Task CrearPrenda(CrearPrendaDto prenda)
        {
            if(prenda != null)
            {
                if (await prendasRepository.ExistePrenda(prenda.Nombre))
                {
                    throw new InvalidOperationException("Ya existe una prenda con ese modelo.");
                }
                var nuevaPrenda = new Prendas
                {
                    ModeloPrenda = prenda.Nombre
                };
                await prendasRepository.CrearPrenda(nuevaPrenda);
            }
            else
            {
                throw new ArgumentNullException(nameof(prenda), "El objeto prenda no puede ser nulo.");
            }
        }

        

        public async Task EditarPrenda(EditarProductoDto prenda)
        {
            var prendaExistente = await prendasRepository.ObtenerPrendas(prenda.Id)
                ?? throw new ArgumentException($"No se encontró una prenda con el ID {prenda.Id}");
            if (await prendasRepository.ExistePrenda(prenda.Nombre, prenda.Id))
            {
                throw new InvalidOperationException("Ya existe otra prenda con ese modelo.");
            }
            prendaExistente.ModeloPrenda = prenda.Nombre;
            await prendasRepository.guardarCambios();
        }

        public Task EliminarPrenda(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Prendas>> ListarPrendas()
        {
           var prendas = prendasRepository.ListarPrendas();
            if(prendas != null)
            {
                return prendas;
            }
            else
            {
                throw new ArgumentException("No se encontraron prendas.");
            }
        }
    }
}
