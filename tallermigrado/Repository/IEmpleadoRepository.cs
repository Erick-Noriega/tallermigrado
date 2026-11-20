using Microsoft.AspNetCore.Identity;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public interface IEmpleadoRepository
    {
        Task<bool> ExisteUserNameOEmail(string userName, string email);
        Task<IdentityResult> CrearEmpleado(Empleados empleado, string password);
        Task<Empleados?> ObtenerPorUserName(string userName);
        Task<string> GenerarTokenConfirmacionEmail(Empleados empleado);
        Task<IdentityResult> ConfirmarEmail(Empleados empleado, string token);
        Task<bool> VerificarPassword(Empleados empleado, string password);
    }
}
