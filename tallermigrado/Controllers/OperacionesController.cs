using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class OperacionesController : Controller
    {
        private readonly IOperacionesService operacionesService;
        private readonly IprendasService prendasService;

        public OperacionesController(IOperacionesService operacionesService, IprendasService prendasService)
        {
            this.operacionesService = operacionesService;
            this.prendasService = prendasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var operaciones = await operacionesService.ListarOperaciones();
            return View(operaciones);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            await CargarPrendas();
            return View(new CrearOperacionDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearOperacionDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarPrendas();
                return View(dto);
            }

            try
            {
                await operacionesService.CrearOperacion(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CargarPrendas();
                return View(dto);
            }

            TempData["Mensaje"] = "Operación creada correctamente.";
            return RedirectToAction(nameof(Crear));
        }

        private async Task CargarPrendas()
        {
            var prendas = await prendasService.ListarPrendas();
            ViewBag.Prendas = new SelectList(prendas, "Id", "ModeloPrenda");
        }
    }
}
