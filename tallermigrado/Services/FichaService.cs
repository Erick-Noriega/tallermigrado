using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class FichaService : IFichaService
    {
        private readonly IFichaRepository fichaRepository;
        public FichaService(IFichaRepository fichaRepository)
        {
            this.fichaRepository = fichaRepository;
        }

        public async Task CrearFicha(CrearFichaDto ficha)
        {
            if (ficha == null)
            {
                throw new ArgumentNullException(nameof(ficha), "El objeto ficha no puede ser nulo.");
            }

            if (await fichaRepository.ExisteFicha(ficha.PrendasId, ficha.TalleId, ficha.ColorId, ficha.EmpresasId, ficha.NumeroDeCorte))
            {
                throw new InvalidOperationException("Ya existe una ficha con la misma prenda, talle, color, empresa y número de corte.");
            }

            if (!int.TryParse(ficha.Cantidad, out var cantidad) || cantidad < 0)
            {
                throw new InvalidOperationException("La cantidad debe ser un número entero mayor o igual a cero.");
            }

            var nuevaFicha = new Ficha
            {
                prendasID = ficha.PrendasId,
                talleId = ficha.TalleId,
                colorId = ficha.ColorId,
                EmpresasId = ficha.EmpresasId,
                ImagenUrl = ficha.ImagenUrl,
                Fecha = ficha.Fecha,
                NumerodeCorte = ficha.NumeroDeCorte,
                cantidad = cantidad
            };
            await fichaRepository.CrearFicha(nuevaFicha);
        }

        public async Task EditarFicha(EditarFichaDto ficha)
        {
            if (ficha == null)
            {
                throw new ArgumentNullException(nameof(ficha), "El objeto ficha no puede ser nulo.");
            }

            var fichaExistente = await fichaRepository.ObtenerFicha(ficha.Id)
                ?? throw new ArgumentException($"No se encontró una ficha con el ID {ficha.Id}");

            if (await fichaRepository.ExisteFicha(ficha.PrendasId, ficha.TalleId, ficha.ColorId, ficha.EmpresasId, ficha.NumeroDeCorte, ficha.Id))
            {
                throw new InvalidOperationException("Ya existe otra ficha con la misma prenda, talle, color, empresa y número de corte.");
            }

            fichaExistente.prendasID = ficha.PrendasId;
            fichaExistente.talleId = ficha.TalleId;
            fichaExistente.colorId = ficha.ColorId;
            fichaExistente.EmpresasId = ficha.EmpresasId;
            fichaExistente.ImagenUrl = ficha.ImagenUrl;
            fichaExistente.Fecha = ficha.Fecha;
            fichaExistente.NumerodeCorte = ficha.NumeroDeCorte;
            fichaExistente.cantidad = int.TryParse(ficha.Cantidad, out var cantidad) && cantidad >= 0
                ? cantidad
                : throw new InvalidOperationException("La cantidad debe ser un número entero mayor o igual a cero.");
            await fichaRepository.guardarCambios();
        }

        public async Task<List<ListarFichaDto>> ListarFichas()
        {
            var fichas = await fichaRepository.ListarFichas();
            return fichas.Select(f => new ListarFichaDto
            {
                Id = f.Id,
                ModeloPrenda = f.prendas?.ModeloPrenda ?? string.Empty,
                NumeroDeCorte = f.NumerodeCorte,
                Cantidad = f.cantidad,
                Talle = f.talle?.DescripcionTalle ?? string.Empty,
                Color = f.Color?.Nombre ?? string.Empty,
                Empresa = f.Empresas?.NombreEmpresa ?? string.Empty,
                Fecha = f.Fecha,
                ImagenUrl = f.ImagenUrl ?? string.Empty
            }).ToList();
        }
    }
}
