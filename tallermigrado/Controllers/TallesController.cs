using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using tallermigrado.Dtos;
using tallermigrado.Services;

namespace tallermigrado.Controllers
{
    [Authorize]
    public class TallesController : Controller
    {
        private readonly ITallesService tallesService;

        public TallesController(ITallesService tallesService)
        {
            this.tallesService = tallesService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var talles = await tallesService.ListarTalles();
            return View(talles);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new CrearTalleDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CrearTalleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                await tallesService.CrearTalle(dto);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(dto);
            }

            TempData["Mensaje"] = "Talle creado correctamente.";
            return RedirectToAction(nameof(Crear));
        }
    }
}
