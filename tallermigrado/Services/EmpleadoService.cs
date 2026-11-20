using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Repository;

namespace tallermigrado.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository empleadoRepository;
        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            this.empleadoRepository = empleadoRepository;
        }

        public async Task CrearEmpleado(CrearEmpleadoDto empleado)
        {
            if (empleado == null)
            {
                throw new ArgumentNullException(nameof(empleado), "El objeto empleado no puede ser nulo.");
            }

            if (await empleadoRepository.ExisteUserNameOEmail(empleado.UserName, empleado.Email))
            {
                throw new InvalidOperationException("Ya existe un empleado con ese usuario o correo.");
            }

            var nuevoEmpleado = new Empleados
            {
                nombre = empleado.Nombre,
                UserName = empleado.UserName,
                Email = empleado.Email
            };
            var resultado = await empleadoRepository.CrearEmpleado(nuevoEmpleado, empleado.Password);
            if (!resultado.Succeeded)
            {
                throw new InvalidOperationException(string.Join(" ", resultado.Errors.Select(e => e.Description)));
            }
        }

        public async Task<string> GenerarTokenConfirmacion(string userName)
        {
            var empleadoExistente = await empleadoRepository.ObtenerPorUserName(userName)
                ?? throw new ArgumentException($"No se encontró un empleado con el usuario {userName}");
            return await empleadoRepository.GenerarTokenConfirmacionEmail(empleadoExistente);
        }

        public async Task ConfirmarEmail(ConfirmarEmailDto confirmacion)
        {
            if (confirmacion == null)
            {
                throw new ArgumentNullException(nameof(confirmacion), "El objeto confirmación no puede ser nulo.");
            }

            var empleadoExistente = await empleadoRepository.ObtenerPorUserName(confirmacion.UserName)
                ?? throw new ArgumentException($"No se encontró un empleado con el usuario {confirmacion.UserName}");
            var resultado = await empleadoRepository.ConfirmarEmail(empleadoExistente, confirmacion.Token);
            if (!resultado.Succeeded)
            {
                throw new InvalidOperationException(string.Join(" ", resultado.Errors.Select(e => e.Description)));
            }
        }

        public async Task<bool> VerificarCredenciales(LoginEmpleadoDto login)
        {
            if (login == null)
            {
                throw new ArgumentNullException(nameof(login), "El objeto login no puede ser nulo.");
            }

            var empleadoExistente = await empleadoRepository.ObtenerPorUserName(login.UserName);
            if (empleadoExistente == null)
            {
                return false;
            }
            return await empleadoRepository.VerificarPassword(empleadoExistente, login.Password);
        }
    }
}
