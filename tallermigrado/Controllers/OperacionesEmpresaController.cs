using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class OperacionesEmpresaController : Controller
    {
        private readonly IOperacionesEmpresaService operacionesEmpresaService;
        private readonly IOperacionesService operacionesService;
        private readonly IEmpresasService empresasService;
        private readonly IprendasService prendasService;

        public OperacionesEmpresaController(
            IOperacionesEmpresaService operacionesEmpresaService,
            IOperacionesService operacionesService,
            IEmpresasService empresasService,
            IprendasService prendasService)
        {
            this.operacionesEmpresaService = operacionesEmpresaService;
            this.operacionesService = operacionesService;
            this.empresasService = empresasService;
            this.prendasService = prendasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var registros = await operacionesEmpresaService.ListarOperacionesEmpresa();
            return View(registros);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            await CargarListas();
            return View(new CrearOperacionEmpresaDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearOperacionEmpresaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CargarListas();
                return View(dto);
            }

            try
            {
                await operacionesEmpresaService.CrearOperacionEmpresa(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await CargarListas();
                return View(dto);
            }

            TempData["Mensaje"] = "Precio de operación registrado correctamente.";
            return RedirectToAction(nameof(Crear));
        }

        private async Task CargarListas()
        {
            var operaciones = await operacionesService.ListarOperaciones();
            var empresas = await empresasService.ListarEmpresas();
            var prendas = await prendasService.ListarPrendas();

            ViewBag.Operaciones = new SelectList(operaciones, "Id", "NombreOperacion");
            ViewBag.Empresas = new SelectList(empresas, "Id", "NombreEmpresa");
            ViewBag.Prendas = new SelectList(prendas, "Id", "ModeloPrenda");
        }
    }
}
