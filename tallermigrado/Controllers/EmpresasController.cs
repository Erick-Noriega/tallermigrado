using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class EmpresasController : Controller
    {
        private readonly IEmpresasService empresasService;

        public EmpresasController(IEmpresasService empresasService)
        {
            this.empresasService = empresasService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var empresas = await empresasService.ListarEmpresas();
            return View(empresas);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new CrearEmpresaDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearEmpresaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await empresasService.CrearEmpresa(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }

            TempData["Mensaje"] = "Empresa creada correctamente.";
            return RedirectToAction(nameof(Crear));
        }
    }
}
