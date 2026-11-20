using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Models;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class ProduccionController : Controller
    {
        private readonly IProduccionEmpleadoService _produccionService;
        private readonly UserManager<Empleados> _userManager;

        public ProduccionController(IProduccionEmpleadoService produccionService, UserManager<Empleados> userManager)
        {
            _produccionService = produccionService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Producción por empleado";
            var producciones = await _produccionService.ObtenerProduccionPorEmpleado();
            return View(producciones);
        }

        public async Task<IActionResult> Registrar()
        {
            ViewData["Title"] = "Registrar Producción";
            var opciones = await _produccionService.ObtenerOpciones();
            return View(opciones);
        }

        [HttpGet]
        public async Task<IActionResult> Opciones(int? prendaId, int? empresaId, int? colorId, int? talleId, string? corte, int? operacionId)
        {
            var opciones = await _produccionService.ObtenerOpcionesPorSeleccion(prendaId, empresaId, colorId, talleId, corte, operacionId);
            return Json(opciones);
        }

        [HttpPost]
        [ActionName("Registrar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPost(CrearProduccionEmpleadoDto dto)
        {
            var empleado = await _userManager.FindByIdAsync(dto.EmpleadoId ?? string.Empty);
            if (empleado == null)
            {
                ModelState.AddModelError(string.Empty, "Debe seleccionar un empleado válido.");
                ViewData["Title"] = "Registrar Producción";
                var opcionesEmpleado = await _produccionService.ObtenerOpciones();
                return View(opcionesEmpleado);
            }

            dto.Fecha = DateTime.Now;

            try
            {
                await _produccionService.RegistrarProduccion(dto);
                TempData["Mensaje"] = "Producción registrada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Title"] = "Registrar Producción";
                var opciones = await _produccionService.ObtenerOpciones();
                return View(opciones);
            }
        }
    }
}
