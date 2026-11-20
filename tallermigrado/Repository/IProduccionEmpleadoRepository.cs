using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IProduccionEmpleadoRepository
    {
        Task<List<Prendas>> ObtenerPrendas();
        Task<List<Empresas>> ObtenerEmpresasPorPrenda(int prendaId);
        Task<List<Colores>> ObtenerColoresPorFicha(int prendaId, int empresaId);
        Task<List<Talles>> ObtenerTallesPorFicha(int prendaId, int empresaId, int colorId);
        Task<List<string>> ObtenerCortesPorFicha(int prendaId, int empresaId, int colorId, int talleId);
        Task<List<Operaciones>> ObtenerOperacionesPorFicha(int prendaId, int empresaId);
        Task<Ficha?> ObtenerFichaExacta(int prendaId, int empresaId, int colorId, int talleId, string corte);
        Task<OperacionesEmpresa?> ObtenerOperacionEmpresa(int operacionId, int empresaId, int prendaId);
        Task<bool> ExisteProduccion(int operacionId, int colorId, int talleId, string corte);
        Task CrearProduccion(ProduccionEmpleados produccion);
        Task<List<Empleados>> ObtenerEmpleados();
        Task<List<ProduccionEmpleados>> ObtenerProduccionPorEmpleado();
        Task<List<ProduccionEmpleados>> ObtenerProduccionPorIds(List<int> ids);
    }
}
