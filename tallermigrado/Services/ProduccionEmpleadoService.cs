using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public interface IProduccionEmpleadoService
    {
        Task<OpcionesProduccionDto> ObtenerOpciones();
        Task<OpcionesProduccionDto> ObtenerOpcionesPorSeleccion(int? prendaId, int? empresaId, int? colorId, int? talleId, string? corte, int? operacionId);
        Task RegistrarProduccion(CrearProduccionEmpleadoDto dto);
        Task<List<ProduccionPorEmpleadoDto>> ObtenerProduccionPorEmpleado();
    }

    public class ProduccionEmpleadoService : IProduccionEmpleadoService
    {
        private readonly IProduccionEmpleadoRepository _repository;

        public ProduccionEmpleadoService(IProduccionEmpleadoRepository repository)
        {
            _repository = repository;
        }

        public async Task<OpcionesProduccionDto> ObtenerOpciones()
        {
            var empleados = await _repository.ObtenerEmpleados();
            return new OpcionesProduccionDto
            {
                Prendas = (await _repository.ObtenerPrendas()).Select(p => new OpcionDto { Id = p.Id, Texto = p.ModeloPrenda }).ToList(),
                Empleados = empleados.Select(e => new EmpleadoOpcionDto { Id = e.Id, Texto = string.IsNullOrWhiteSpace(e.nombre) ? e.UserName ?? e.Id : e.nombre }).ToList()
            };
        }

        public async Task<OpcionesProduccionDto> ObtenerOpcionesPorSeleccion(int? prendaId, int? empresaId, int? colorId, int? talleId, string? corte, int? operacionId)
        {
            var opciones = new OpcionesProduccionDto();

            if (prendaId == null)
            {
                return opciones;
            }

            opciones.Empresas = (await _repository.ObtenerEmpresasPorPrenda(prendaId.Value))
                .Select(e => new OpcionDto { Id = e.Id, Texto = e.NombreEmpresa }).ToList();

            if (empresaId == null)
            {
                return opciones;
            }

            opciones.Colores = (await _repository.ObtenerColoresPorFicha(prendaId.Value, empresaId.Value))
                .Select(c => new OpcionDto { Id = c.Id, Texto = c.Nombre }).ToList();

            if (colorId == null)
            {
                return opciones;
            }

            opciones.Talles = (await _repository.ObtenerTallesPorFicha(prendaId.Value, empresaId.Value, colorId.Value))
                .Select(t => new OpcionDto { Id = t.Id, Texto = t.DescripcionTalle }).ToList();

            if (talleId == null)
            {
                return opciones;
            }

            opciones.Cortes = (await _repository.ObtenerCortesPorFicha(prendaId.Value, empresaId.Value, colorId.Value, talleId.Value))
                .Select((corte, indice) => new OpcionDto { Id = indice, Texto = corte }).ToList();
            opciones.Operaciones = (await _repository.ObtenerOperacionesPorFicha(prendaId.Value, empresaId.Value))
                .Select(o => new OpcionDto { Id = o.Id, Texto = o.NombreOperacion }).ToList();

            if (corte == null)
            {
                return opciones;
            }

            var ficha = await _repository.ObtenerFichaExacta(prendaId.Value, empresaId.Value, colorId.Value, talleId.Value, corte);
            opciones.Cantidad = ficha?.cantidad;

            if (operacionId == null)
            {
                return opciones;
            }

            var operacionEmpresa = await _repository.ObtenerOperacionEmpresa(operacionId.Value, empresaId.Value, prendaId.Value);
            opciones.Precio = operacionEmpresa?.precio;

            return opciones;
        }

        public async Task RegistrarProduccion(CrearProduccionEmpleadoDto dto)
        {
            if (string.IsNullOrEmpty(dto.EmpleadoId))
            {
                throw new InvalidOperationException("Debe seleccionar el empleado que realiza la producción.");
            }

            var ficha = await _repository.ObtenerFichaExacta(dto.PrendasId, dto.EmpresasId, dto.ColorId, dto.TalleId, dto.Corte);

            if (ficha == null)
            {
                throw new InvalidOperationException("La combinación seleccionada no existe en Ficha.");
            }

            if (ficha.cantidad <= 0)
            {
                throw new InvalidOperationException("La ficha seleccionada no tiene cantidad de prendas.");
            }

            var operacionEmpresa = await _repository.ObtenerOperacionEmpresa(dto.OperacionId, dto.EmpresasId, dto.PrendasId);

            if (await _repository.ExisteProduccion(dto.OperacionId, dto.ColorId, dto.TalleId, dto.Corte))
            {
                throw new InvalidOperationException("Esta producción ya fue registrada por otro empleado.");
            }

            await _repository.CrearProduccion(new ProduccionEmpleados
            {
                empleadoId = dto.EmpleadoId,
                operacionesId = dto.OperacionId,
                coloresId = dto.ColorId,
                talleId = dto.TalleId,
                prendasId = dto.PrendasId,
                empresasId = dto.EmpresasId,
                corte = dto.Corte,
                CantidadDePrendas = ficha.cantidad,
                Fecha = dto.Fecha,
                precio = operacionEmpresa?.precio
            });
        }

        public async Task<List<ProduccionPorEmpleadoDto>> ObtenerProduccionPorEmpleado()
        {
            var registros = await _repository.ObtenerProduccionPorEmpleado();

            return registros
                .GroupBy(p => p.empleadoId)
                .Select(g => new ProduccionPorEmpleadoDto
                {
                    EmpleadoId = g.Key,
                    NombreEmpleado = g.First().empleados == null ? g.Key : (string.IsNullOrWhiteSpace(g.First().empleados!.nombre) ? g.First().empleados!.UserName ?? g.Key : g.First().empleados!.nombre),
                    Registros = g.Select(p => new ProduccionItemDto
                    {
                        Id = p.Id,
                        Operacion = p.Operaciones?.NombreOperacion ?? string.Empty,
                        Color = p.color?.Nombre ?? string.Empty,
                        Talle = p.talle?.DescripcionTalle ?? string.Empty,
                        Corte = p.corte,
                        Empresa = p.Empresas?.NombreEmpresa ?? "Sin empresa",
                        Prenda = p.Prendas?.ModeloPrenda ?? "Sin prenda",
                        Cantidad = p.CantidadDePrendas,
                        Precio = p.precio,
                        Fecha = p.Fecha
                    }).ToList()
                })
                .ToList();
        }
    }
}
