using tallermigrado.Dtos;
using tallermigrado.Models;

namespace tallermigrado.Services
{
    public interface IprendasService
    {
        Task<List<Prendas>> ListarPrendas();
        Task CrearPrenda(CrearPrendaDto prenda);
        Task EditarPrenda(EditarProductoDto prenda);
        Task EliminarPrenda(int id);
    }
}
