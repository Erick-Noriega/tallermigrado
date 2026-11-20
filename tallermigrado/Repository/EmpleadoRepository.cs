using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tallermigrado.Models;

namespace tallermigrado.Repository
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly UserManager<Empleados> _userManager;
        public EmpleadoRepository(UserManager<Empleados> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> ExisteUserNameOEmail(string userName, string email)
        {
            return await _userManager.Users.AnyAsync(u => u.UserName == userName || u.Email == email);
        }

        public async Task<IdentityResult> CrearEmpleado(Empleados empleado, string password)
        {
            var resultado = await _userManager.CreateAsync(empleado, password);
            if (resultado.Succeeded)
            {
                await _userManager.AddToRoleAsync(empleado, "Empleado");
            }
            return resultado;
        }

        public async Task<Empleados?> ObtenerPorUserName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<string> GenerarTokenConfirmacionEmail(Empleados empleado)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(empleado);
        }

        public async Task<IdentityResult> ConfirmarEmail(Empleados empleado, string token)
        {
            return await _userManager.ConfirmEmailAsync(empleado, token);
        }

        public async Task<bool> VerificarPassword(Empleados empleado, string password)
        {
            return await _userManager.CheckPasswordAsync(empleado, password);
        }
    }
}
