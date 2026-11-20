using tallermigrado.Dtos;

namespace tallermigrado.Services
{
    public interface IFichaService
    {
        Task<List<ListarFichaDto>> ListarFichas();
        Task CrearFicha(CrearFichaDto ficha);
        Task EditarFicha(EditarFichaDto ficha);
    }
}
