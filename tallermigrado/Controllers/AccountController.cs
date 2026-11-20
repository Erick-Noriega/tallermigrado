using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmpleadoService empleadoService;
        private readonly SignInManager<Empleados> signInManager;

        public AccountController(IEmpleadoService empleadoService, SignInManager<Empleados> signInManager)
        {
            this.empleadoService = empleadoService;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new CrearEmpleadoDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(CrearEmpleadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await empleadoService.CrearEmpleado(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }

            TempData["Mensaje"] = "Empleado registrado correctamente. Ya puedes iniciar sesión.";
            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginEmpleadoDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginEmpleadoDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var resultado = await signInManager.PasswordSignInAsync(dto.UserName, dto.Password, isPersistent: false, lockoutOnFailure: false);
            if (resultado.Succeeded)
            {
                return RedirectToAction("Index", "Produccion");
            }

            ModelState.AddModelError(string.Empty, "Usuario o contraseña incorrectos.");
            return View(dto);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
