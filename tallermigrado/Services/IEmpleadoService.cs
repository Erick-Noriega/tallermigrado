using tallermigrado.Dtos;

namespace tallermigrado.Services
{
    public interface IEmpleadoService
    {
        Task CrearEmpleado(CrearEmpleadoDto empleado);
        Task<string> GenerarTokenConfirmacion(string userName);
        Task ConfirmarEmail(ConfirmarEmailDto confirmacion);
        Task<bool> VerificarCredenciales(LoginEmpleadoDto login);
    }
}
